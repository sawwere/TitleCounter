package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.film.*;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.service.FilmService;
import com.sawwere.titlecounter.common.dto.film.FilmDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryRequestDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryResponseDto;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@RestController
@RequestMapping(produces="application/json")
@RequiredArgsConstructor
public class FilmController {
    private final FilmService filmService;

    private final FilmDtoFactory filmDtoFactory;
    private final FilmEntryDtoFactory filmEntryDtoFactory;

    public static final String CREATE_FILM = "/api/films";
    public static final String UPDATE_FILM = "/api/films/{film_id}";
    public static final String FIND_FILM = "/api/films/{film_id}";
    public static final String DELETE_FILM = "/api/films/{film_id}";
    public static final String FIND_ALL_FILMS = "/api/films";
    public static final String CREATE_FILM_ENTRY = "/api/users/{username}/films";
    public static final String UPDATE_FILM_ENTRY = "/api/films/submissions/{submission_id}";
    public static final String DELETE_FILM_ENTRY = "/api/films/submissions/{submission_id}";
    public static final String FIND_FILM_ENTRIES = "/api/users/{username}/films";
    public static final String AUTO_CREATE_FILM = "/api/films/auto-create";
    /**
     * Обрабатывает входящеий запрос на создание нового Film.
     * @param filmDto объект, содержащий необходимые для создания Film данные
     * @return FilmDto, содержащий данные созданной Film
     */
    @PostMapping(CREATE_FILM)
    @ResponseStatus(HttpStatus.CREATED)
    public FilmDto createFilm(@Valid @RequestPart("film") FilmDto filmDto,
                              @RequestPart("image") MultipartFile image) {
        var filmEntity = filmService.createFilm(filmDto, image);
        return filmDtoFactory.entityToDto(filmEntity);
    }
    @PostMapping(AUTO_CREATE_FILM)
    public FilmDto autoCreateFilm(@Valid @RequestParam(value = "title") String title) {
        var filmEntity = filmService.autoCreateFilm(title);
        return filmDtoFactory.entityToDto(filmEntity);
    }

    @PutMapping(UPDATE_FILM)
    public  FilmDto putFilm(@PathVariable(value = "film_id") Long filmId,
                            @Valid @RequestBody FilmDto filmDto) {
        return filmDtoFactory.entityToDto(filmService.updateFilm(filmId, filmDto));
    }

    @GetMapping(FIND_FILM)
    public FilmDto findFilm(@PathVariable(value = "film_id") Long filmId) {
        return filmDtoFactory.entityToDto(filmService.findOrElseThrowException(filmId));
    }

    @DeleteMapping(DELETE_FILM)
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void deleteFilm(@PathVariable("film_id") Long filmId) {
        filmService.deleteFilm(filmId);
    }

    @GetMapping(FIND_ALL_FILMS)
    public List<FilmDto> findAllFilms(@RequestParam(value = "q", required = false) Optional<String> query) {
        return filmService.search(query).stream().map(filmDtoFactory::entityToDto).collect(Collectors.toList());
    }

    @GetMapping(FIND_FILM_ENTRIES)
    public List<FilmEntryResponseDto> findFilmEntriesByUser(@PathVariable(name="username") String username) {
        return filmService
                .findFilmEntriesByUser(username)
                .stream()
                .map(filmEntryDtoFactory::entityToDto)
                .collect(Collectors.toList());
    }

    @PostMapping(CREATE_FILM_ENTRY)
    public FilmEntryResponseDto createFilmEntry(@PathVariable(name="username") String username,
                                                @RequestBody FilmEntryRequestDto filmEntryDto,
                                                Authentication authentication) {
        if (!authentication.getName().equals(username))
        {
            throw new ForbiddenException("Access denied");
        }
        return filmEntryDtoFactory.entityToDto(filmService.createFilmEntry(username, filmEntryDto));
    }

    @PutMapping(UPDATE_FILM_ENTRY)
    public void putFilmEntry(@PathVariable(name="submission_id") Long filmEntryId,
                             @RequestBody FilmEntryRequestDto filmEntryDto,
                             Authentication authentication) {
        filmService.updateFilmEntry(filmEntryId, filmEntryDto, authentication);
    }

    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping(DELETE_FILM_ENTRY)
    public void createFilmEntry(@PathVariable(name="submission_id") Long filmEntryId,
                                Authentication authentication) {
        filmService.deleteFilmEntry(filmEntryId, authentication);
    }
}
