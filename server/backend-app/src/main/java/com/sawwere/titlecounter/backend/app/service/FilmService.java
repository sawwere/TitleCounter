package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.film.FilmCreationDto;
import com.sawwere.titlecounter.backend.app.dto.film.FilmDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.film.FilmEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import com.sawwere.titlecounter.backend.app.storage.entity.FilmEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.FilmStatisticsAggregationResult;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.FilmEntryRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.FilmRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.specification.FilmSpecification;
import com.sawwere.titlecounter.common.dto.film.FilmDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryRequestDto;
import feign.FeignException;
import jakarta.validation.Valid;
import java.util.List;
import java.util.Objects;
import java.util.Optional;
import java.util.logging.Logger;
import java.util.stream.Stream;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;


@Service
@RequiredArgsConstructor
public class FilmService {
    private static final Logger LOGGER =
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
    public Film createFilm(@Valid FilmCreationDto filmDto) {
        Film film = filmDtoFactory.creationDtoToEntity(filmDto);
        filmRepository.save(film);
        LOGGER.info(filmDto.getImageUrl());

        var image = externalContentSearchService.findImage(filmDto.getImageUrl());
        imageStorageService.store(image, "films/%d".formatted(film.getId()));
        LOGGER.info("Created film '%s' with id '%d' kpId '%s'"
                .formatted(film.getTitle(), film.getId(), film.getExternalId().getKpId())
        );
        return film;
    }

    @Transactional
    public void autoCreateFilm(String title) {
        var list = externalContentSearchService.findFilms(title, null);
        if (list.getTotal() == 0) {
            throw new NotFoundException(title);
        }
        FilmCreationDto dto = list.getContents().getFirst();
        if (filmRepository.findByExternalId_KpId(dto.getExternalId().getKpId()).isEmpty()) {
            createFilm(dto);
        }
    }


    public void autoCreateFilm(int pageLimit, int limit) {
        for (int page = pageLimit; page < pageLimit + limit; page++) {
            try {
                var list = externalContentSearchService.findFilms(null, String.valueOf(page));
                if (list.getTotal() == 0) {
                    throw new NotFoundException(String.valueOf(page));
                }
                for (int ind = 0; ind < list.getTotal(); ind++) {
                    FilmCreationDto dto = list.getContents().get(ind);
                    String tmdbId = dto.getExternalId().getTmdbId();

                    if (filmRepository.findByExternalId_KpId(dto.getExternalId().getKpId()).isPresent()) {
                        LOGGER.severe("Already exists at page '%d' kpId %s "
                                .formatted(page, dto.getExternalId().getKpId()));
                    } else if (tmdbId != null
                            && filmRepository.findByExternalId_TmdbId(dto.getExternalId().getTmdbId()).isPresent()) {
                        LOGGER.severe("Already exists at page '%d' kpId %s tmdbId %s"
                                .formatted(page, dto.getExternalId().getKpId(), dto.getExternalId().getTmdbId()));
                    } else {
                        createFilm(dto);
                    }
                }

            } catch (FeignException.FeignClientException ex) {
                LOGGER.severe("NOT FOUND page '%d'"
                        .formatted(page));
                break;
            } catch (RuntimeException ex) {
                LOGGER.severe("Error at page '%d'"
                        .formatted(page));
                throw new RuntimeException(ex.getMessage());
            }
        }
    }

    //TODO
    @Transactional
    public Film updateFilm(Long filmId, @Valid FilmDto filmDto) {
        filmDto.setId(filmId);
        LOGGER.info("Updated film '%s' with id '%d'".formatted(filmDto.getTitle(), filmDto.getId()));
        return filmRepository.save(filmDtoFactory.dtoToEntity(filmDto));
    }

    @Transactional
    public void deleteFilm(Long filmId) {
        Film film = findOrElseThrowException(filmId);
        filmRepository.deleteById(filmId);
        LOGGER.info("Deleted film '%s' with id '%d'".formatted(film.getTitle(), filmId));
    }

    @Transactional(readOnly = true)
    public List<Film> findAll() {
        return filmRepository.streamAllBy().toList();
    }

    @Transactional(readOnly = true)
    public Page<Film> search(String title,
                             int page, int pageSize) {
        Specification<Film> filter = Specification.where(null);
        if (title != null) {
            filter = filter.and(FilmSpecification.titleContains(title));
        }
        Pageable pageable = PageRequest.of(page, pageSize);
        return filmRepository.findAll(filter, pageable);
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
        LOGGER.info("Created FilmEntry %d for user %s".formatted(filmEntry.getId(), username));
        return filmEntry;
    }

    @Transactional
    public void updateFilmEntry(Long filmEntryId,
                                @Valid FilmEntryRequestDto filmEntryDto,
                                Authentication authentication) {
        User user = userService.findUserByUsername(authentication.getName());
        if (!user.getId().equals(filmEntryDto.getUserId())) {
            throw new ForbiddenException();
        }
        if (!Objects.equals(filmEntryId, filmEntryDto.getId())) {
            throw new IllegalArgumentException("Invalid id passed");
        }
        FilmEntry filmEntry = findEntryOrElseThrowException(filmEntryId);
        filmEntry.setCustomTitle(filmEntryDto.getCustomTitle());
        filmEntry.setNote(filmEntryDto.getNote());
        filmEntry.setScore(filmEntryDto.getScore());
        filmEntry.setStatus(filmEntryDto.getStatus());
        filmEntry.setDateCompleted(filmEntryDto.getDateCompleted());
        filmEntryRepository.save(filmEntry);
        LOGGER.info("Updated FilmEntry %d for user %s".formatted(filmEntry.getId(), user.getUsername()));
    }

    @Transactional
    public void deleteFilmEntry(Long filmEntryId, Authentication authentication) {
        FilmEntry filmEntry = findEntryOrElseThrowException(filmEntryId);
        User user = userService.findUserByUsername(authentication.getName());
        if (!user.getId().equals(filmEntry.getUser().getId())) {
            throw new ForbiddenException();
        }
        filmEntryRepository.delete(filmEntry);
        LOGGER.info("Deleted FilmEntry %d for user %s".formatted(filmEntry.getId(), user.getUsername()));
    }

    @Transactional(readOnly = true)
    public List<FilmEntry> findFilmEntriesByUser(String username) {
        User user = userService.findUserByUsername(username);
        Stream<FilmEntry> filmEntries = filmEntryRepository.streamAllByUserId(user.getId());
        return filmEntries.toList();
    }

    @Transactional(readOnly = true)
    public List<Film> findFilmsByUser(String username) {
        User user = userService.findUserByUsername(username);
        List<FilmEntry> filmEntries = findFilmEntriesByUser(username);

        return filmEntries.stream().map(FilmEntry::getFilm).toList();
    }

    public List<FilmStatisticsAggregationResult> getStatistics() {
        return filmRepository.getStatistics();
    }

    public void updateStatistics() {
        for (var ar : getStatistics()) {
            Film film = findOrElseThrowException(ar.getFilmId());
            film.setGlobalScore(ar.getGlobalScore());
            filmRepository.save(film);
        }
    }
}
