package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.SearchResponseDto;
import com.sawwere.titlecounter.backend.app.dto.film.FilmCreationDto;
import com.sawwere.titlecounter.backend.app.dto.film.FilmDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.film.FilmEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.service.FilmService;
import com.sawwere.titlecounter.backend.app.service.ImageStorageService;
import com.sawwere.titlecounter.common.dto.film.FilmDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryRequestDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryResponseDto;
import io.swagger.v3.oas.annotations.Hidden;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import java.util.List;
import java.util.stream.Collectors;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.Authentication;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;


@RestController
@RequestMapping(produces = "application/json")
@RequiredArgsConstructor
public class FilmController {
    private final ImageStorageService imageStorageService;
    private final FilmService filmService;

    private final FilmDtoFactory filmDtoFactory;
    private final FilmEntryDtoFactory filmEntryDtoFactory;

    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String CREATE_FILM = "/api/films";
    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String UPDATE_FILM = "/api/films/{film_id}";
    public static final String FIND_FILM = "/api/films/{film_id}";
    public static final String DELETE_FILM = "/api/films/{film_id}";
    public static final String FIND_ALL_FILMS = "/api/films";
    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String CREATE_FILM_ENTRY = "/api/users/{username}/films";
    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String UPDATE_FILM_ENTRY = "/api/films/submissions/{submission_id}";
    public static final String DELETE_FILM_ENTRY = "/api/films/submissions/{submission_id}";
    public static final String FIND_FILM_ENTRIES = "/api/users/{username}/films";
    public static final String AUTO_CREATE_FILM = "/api/films/auto-create";

    /**
     * Обрабатывает входящеий запрос на создание нового Film.
     * @param filmDto объект, содержащий необходимые для создания Film данные
     * @return FilmDto, содержащий данные созданной Film
     */
    @Operation(
            summary = "Create film",
            description = "Use to create new film"
    )
    @PostMapping(CREATE_FILM)
    @ResponseStatus(HttpStatus.CREATED)
    public FilmDto createFilm(@Valid @RequestPart("film") FilmCreationDto filmDto,
                              @RequestPart(value = "image", required = false) MultipartFile image) {
        var filmEntity = filmService.createFilm(filmDto);
        if (image != null) {
            var id = filmEntity.getId();
            imageStorageService.store(image, "films/%d".formatted(id));
        }
        return filmDtoFactory.entityToDto(filmEntity);
    }

    /**
     * Film update
     * @param filmId Film id
     * @param filmDto Dto, from which data for update will be taken
     * @return updated or created film
     */
    @Operation(
            summary = "Update film",
            description = "Use to update existing film"
    )
    @PutMapping(UPDATE_FILM)
    public  FilmDto putFilm(@PathVariable(value = "film_id") Long filmId,
                            @Valid @RequestBody FilmDto filmDto) {
        return filmDtoFactory.entityToDto(filmService.updateFilm(filmId, filmDto));
    }

    /**
     * Find film by id
     * @param filmId Film id
     * @return Film with specified id
     * @throws NotFoundException in case there is no film with such id
     */
    @Operation(
            summary = "Find film",
            description = "Use to find film by id"
    )
    @GetMapping(FIND_FILM)
    public FilmDto findFilm(@PathVariable(value = "film_id") Long filmId) {
        return filmDtoFactory.entityToDto(filmService.findOrElseThrowException(filmId));
    }

    /**
     * Delete film by id
     * @param filmId Film id
     * @throws NotFoundException in case there is no film with such id
     */
    @DeleteMapping(DELETE_FILM)
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void deleteFilm(@PathVariable("film_id") Long filmId) {
        filmService.deleteFilm(filmId);
    }

    /**
     * Search films
     * @param title Title of the film
     * @return List of films that meet the conditions
     */
    @GetMapping(FIND_ALL_FILMS)
    public SearchResponseDto<FilmDto> searchFilms(
            @RequestParam(value = "title", required = false)
            @Parameter(description = "Title of the film") String title,

            @RequestParam(required = false, defaultValue = "0")
            @Validated @Min(0) Integer page,
            @RequestParam(value = "page_size", required = false, defaultValue = "10")
            @Validated @Min(0) @Max(100) Integer pageSize) {
        var searchResult = filmService.search(title, page, pageSize);
        return new SearchResponseDto<>(
                searchResult.getTotalPages(),
                searchResult.stream().map(filmDtoFactory::entityToDto).toList()
        );
    }

    @Operation(
            summary = "Search film entries by user",
            description = "Use to search film entries by user "
    )
    @GetMapping(FIND_FILM_ENTRIES)
    public List<FilmEntryResponseDto> findFilmEntriesByUser(@PathVariable(name = "username") String username) {
        return filmService
                .findFilmEntriesByUser(username)
                .stream()
                .map(filmEntryDtoFactory::entityToDto)
                .collect(Collectors.toList());
    }

    @Operation(
            summary = "Create new film entry for user",
            description = "Use to create film entries for users"
    )
    @PostMapping(CREATE_FILM_ENTRY)
    public FilmEntryResponseDto createFilmEntry(@PathVariable(name = "username") String username,
                                                @RequestBody FilmEntryRequestDto filmEntryDto,
                                                Authentication authentication) {
        if (!authentication.getName().equals(username)) {
            throw new ForbiddenException("Access denied");
        }
        return filmEntryDtoFactory.entityToDto(filmService.createFilmEntry(username, filmEntryDto));
    }

    @Operation(
            summary = "Update film entry",
            description = "Use to update film entries. Auth required"
    )
    @PutMapping(UPDATE_FILM_ENTRY)
    public void putFilmEntry(@PathVariable(name = "submission_id") Long filmEntryId,
                             @RequestBody FilmEntryRequestDto filmEntryDto,
                             Authentication authentication) {
        filmService.updateFilmEntry(filmEntryId, filmEntryDto, authentication);
    }

    @Operation(
            summary = "Delete film entry",
            description = "Use to delete film entries. Auth required"
    )
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping(DELETE_FILM_ENTRY)
    public void createFilmEntry(@PathVariable(name = "submission_id") Long filmEntryId,
                                Authentication authentication) {
        filmService.deleteFilmEntry(filmEntryId, authentication);
    }

    @Hidden
    @GetMapping(AUTO_CREATE_FILM + "/title")
    public void autoCreate(@RequestParam(value = "title", required = false) String title) {
        filmService.autoCreateFilm(title);
    }

    @Hidden
    @GetMapping(AUTO_CREATE_FILM + "/page")
    public void autoCreate(@RequestParam(value = "page", required = false) Integer page,
                           @RequestParam(value = "limit", required = false) Integer limit) {
        filmService.autoCreateFilm(page, limit);
    }
}
