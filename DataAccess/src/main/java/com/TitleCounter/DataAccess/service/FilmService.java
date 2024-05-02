package com.TitleCounter.DataAccess.service;

import com.TitleCounter.DataAccess.dto.film.*;
import com.TitleCounter.DataAccess.exception.ForbiddenException;
import com.TitleCounter.DataAccess.exception.NotFoundException;
import com.TitleCounter.DataAccess.storage.entity.Film;
import com.TitleCounter.DataAccess.storage.entity.FilmEntry;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.FilmEntryRepository;
import com.TitleCounter.DataAccess.storage.repository.FilmRepository;
import jakarta.transaction.Transactional;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;

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
    public Film createFilm(@Valid FilmDto filmDto) {
        Film film = filmDtoFactory.dtoToEntity(filmDto);
        filmRepository.save(film);
        logger.info("Created film '%s' with id '%d'".formatted(film.getTitle(), film.getId()));
        return film;
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

    @Transactional
    public List<Film> findAll() {
        return filmRepository.streamAllBy().toList();
    }

    @Transactional
    public List<Film> search(Optional<String> query) {
        var films = filmRepository.streamAllBy();
        if (query.isPresent()) {
            films = films.filter(_film-> _film.getTitle().equals(query.get()));
        }
        return films.toList();
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

    @Transactional
    public List<FilmEntry> findFilmEntriesByUser(String username) {
        User user = userService.findUserByUsername(username);
        Stream<FilmEntry> filmEntries = filmEntryRepository.streamAllByUserId(user.getId());
        return filmEntries.toList();
    }

    @Transactional
    public List<Film> findFilmsByUser(String username) {
        User user = userService.findUserByUsername(username);
        List<FilmEntry> filmEntries = findFilmEntriesByUser(username);

        return filmEntries.stream().map(x->x.getFilm()).toList();
    }
}
