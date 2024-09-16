package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.film.FilmCreationDto;
import com.sawwere.titlecounter.backend.app.dto.film.FilmDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.film.FilmEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import com.sawwere.titlecounter.backend.app.storage.entity.FilmEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.FilmEntryRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.FilmRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.specification.FilmSpecification;
import com.sawwere.titlecounter.common.dto.film.FilmDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryRequestDto;
import feign.FeignException;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.hibernate.engine.jdbc.spi.SqlExceptionHelper;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.multipart.MultipartFile;

import java.sql.SQLException;
import java.util.List;
import java.util.Objects;
import java.util.Optional;
import java.util.logging.Logger;
import java.util.stream.Stream;

@Service
@RequiredArgsConstructor
public class FilmService {
    private static final Logger logger =
            Logger.getLogger(FilmService.class.getName());

    private final FilmRepository filmRepository;
    private final FilmEntryRepository filmEntryRepository;

    private final FilmDtoFactory filmDtoFactory;
    private final FilmEntryDtoFactory filmEntryDtoFactory;

    private final ImageStorageService imageStorageService;
    private final ExternalContentSearchService externalContentSearchService;
    private final UserService userService;
    public Optional<Film> findFilm(Long filmId) {
        return filmRepository.findById(filmId);
    }

    public Film findOrElseThrowException(Long filmId) throws NotFoundException {
        return filmRepository.findById(filmId)
                .orElseThrow(() -> new NotFoundException(String.format("Film with id '%s' doesn't exist", filmId))
                );
    }

    public FilmEntry findEntryOrElseThrowException(Long filmEntryId) throws NotFoundException {
        return filmEntryRepository.findById(filmEntryId)
                .orElseThrow(() -> new NotFoundException(
                        String.format("FilmEntry with id '%s' doesn't exist", filmEntryId))
                );
    }

    @Transactional
    public Film createFilm(@Valid FilmDto filmDto, MultipartFile image) {
        Film film = filmDtoFactory.dtoToEntity(filmDto);
        filmRepository.save(film);
        imageStorageService.store(image, "films/%d".formatted(film.getId()));
        logger.info("Created film '%s' with id '%d'".formatted(film.getTitle(), film.getId()));
        return film;
    }

    @Transactional
    public void autoCreateFilm(String title) {
        var list = externalContentSearchService.findFilms(title, null);
        if (list.getTotal() == 0)
            throw new NotFoundException(title);
        FilmCreationDto dto = list.getContents().get(0);
        if (filmRepository.findByExternalId_KpId(dto.getExternalId().getKpId()).isEmpty()) {
            Film film = filmDtoFactory.creationDtoToEntity(dto);
            filmRepository.save(film);
            System.out.println(dto.getImageUrl());
            var image = externalContentSearchService.findImage(dto.getImageUrl());
            imageStorageService.store(image, "films/%d".formatted(film.getId()));
            logger.info("Created film '%s' with id '%d' kpId '%s'"
                    .formatted(film.getTitle(), film.getId(), film.getExternalId().getKpId())
            );
        }
    }


    public void autoCreateFilm(int pageLimit, int limit) {
        for (int page = pageLimit; page < pageLimit + limit; page++) {
            try {
                var list = externalContentSearchService.findFilms(null, String.valueOf(page));
                if (list.getTotal() == 0)
                    throw new NotFoundException(String.valueOf(page));
                for (int ind = 0; ind < list.getTotal(); ind++) {
                    FilmCreationDto dto = list.getContents().get(ind);
                    String tmdb_id = dto.getExternalId().getTmdbId();

                    if (filmRepository.findByExternalId_KpId(dto.getExternalId().getKpId()).isPresent())
                        logger.severe("NOT FOUND page '%d' kpId %s "
                                .formatted(page, dto.getExternalId().getKpId()));
                    else if (tmdb_id != null && filmRepository.findByExternalId_TmdbId(dto.getExternalId().getTmdbId()).isPresent())
                        logger.severe("NOT FOUND page '%d' kpId %s tmdbId %s"
                                .formatted(page, dto.getExternalId().getKpId(), dto.getExternalId().getTmdbId()));
                    else {
                        Film film = filmDtoFactory.creationDtoToEntity(dto);
                        filmRepository.save(film);
                        System.out.println(dto.getImageUrl());
                        var image = externalContentSearchService.findImage(dto.getImageUrl());
                        imageStorageService.store(image, "films/%d".formatted(film.getId()));
                        logger.info("Created film '%s' with id '%d' kpId '%s' on page %d"
                                .formatted(film.getTitle(), film.getId(), film.getExternalId().getKpId(), page)
                        );
                    }
                }

            } catch (FeignException.FeignClientException ex) {
                logger.severe("NOT FOUND page '%d'"
                        .formatted(page));
                break;
            }
        }
    }

    //TODO
    @Transactional
    public Film updateFilm(Long filmId, @Valid FilmDto filmDto) {
        filmDto.setId(filmId);
        logger.info("Updated film '%s' with id '%d'".formatted(filmDto.getTitle(), filmDto.getId()));
        return filmRepository.save(filmDtoFactory.dtoToEntity(filmDto));
    }

    @Transactional
    public void deleteFilm(Long filmId) {
        Film film = findOrElseThrowException(filmId);
        filmRepository.deleteById(filmId);
        logger.info("Deleted film '%s' with id '%d'".formatted(film.getTitle(), filmId));
    }

    @Transactional(readOnly=true)
    public List<Film> findAll() {
        return filmRepository.streamAllBy().toList();
    }

    @Transactional(readOnly=true)
    public List<Film> search(Optional<String> query) {
        Specification<Film> filter = Specification.where(null);
        if (query.isPresent()) {
            filter = filter.and(FilmSpecification.titleContains(query.get()));
        }
        return filmRepository.findAll(filter, Pageable.ofSize(10)).getContent();
    }

    @Transactional
    public FilmEntry createFilmEntry(String username, FilmEntryRequestDto filmEntryDto) {
        FilmEntry filmEntry = filmEntryDtoFactory.dtoToEntity(filmEntryDto);
        Film filmEntity = findOrElseThrowException(filmEntryDto.getFilmId());
        filmEntry.setFilm(filmEntity);
        filmEntry.setCustomTitle(filmEntity.getTitle());

        User user = userService.findUserByUsername(username);
        filmEntry.setUser(user);
        filmEntryRepository.save(filmEntry);
        logger.info("Created FilmEntry %d for user %s".formatted(filmEntry.getId(), username));
        return filmEntry;
    }

    @Transactional
    public void updateFilmEntry(Long filmEntryId, @Valid FilmEntryRequestDto filmEntryDto, Authentication authentication) {
        User user = userService.findUserByUsername(authentication.getName());
        if (!user.getId().equals(filmEntryDto.getUserId()))
            throw new ForbiddenException("You don't have access to requested resource");
        if (!Objects.equals(filmEntryId, filmEntryDto.getId()))
            throw new IllegalArgumentException("Invalid id passed");
        FilmEntry filmEntry = findEntryOrElseThrowException(filmEntryId);
        filmEntry.setCustomTitle(filmEntryDto.getCustomTitle());
        filmEntry.setNote(filmEntryDto.getNote());
        filmEntry.setScore(filmEntryDto.getScore());
        filmEntry.setStatus(filmEntryDto.getStatus());
        filmEntry.setDateCompleted(filmEntryDto.getDateCompleted());
        filmEntryRepository.save(filmEntry);
        logger.info("Updated FilmEntry %d for user %s".formatted(filmEntry.getId(), user.getUsername()));
    }

    @Transactional
    public void deleteFilmEntry(Long filmEntryId, Authentication authentication) {
        FilmEntry filmEntry = findEntryOrElseThrowException(filmEntryId);
        User user = userService.findUserByUsername(authentication.getName());
        if (!user.getId().equals(filmEntry.getUser().getId()))
            throw new ForbiddenException("You don't have access to requested resource");
        filmEntryRepository.delete(filmEntry);
        logger.info("Deleted FilmEntry %d for user %s".formatted(filmEntry.getId(), user.getUsername()));
    }

    @Transactional(readOnly=true)
    public List<FilmEntry> findFilmEntriesByUser(String username) {
        User user = userService.findUserByUsername(username);
        Stream<FilmEntry> filmEntries = filmEntryRepository.streamAllByUserId(user.getId());
        return filmEntries.toList();
    }

    @Transactional(readOnly=true)
    public List<Film> findFilmsByUser(String username) {
        User user = userService.findUserByUsername(username);
        List<FilmEntry> filmEntries = findFilmEntriesByUser(username);

        return filmEntries.stream().map(x->x.getFilm()).toList();
    }
}
