package com.sawwere.titlecounter.backend.app.controller.api;


import com.sawwere.titlecounter.backend.app.dto.SearchResponseDto;
import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.dto.game.GameEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.mapper.GameMapper;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.service.GameService;
import com.sawwere.titlecounter.backend.app.service.ImageStorageService;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryRequestDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryResponseDto;
import io.swagger.v3.oas.annotations.Hidden;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import java.time.LocalDate;
import java.util.List;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.Authentication;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.CrossOrigin;
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
@CrossOrigin
@RequestMapping(produces = "application/json")
@RequiredArgsConstructor
@Tag(name = "GameController",
        description = "Use to work with games and game entries")
public class GameController {
    private final ImageStorageService imageStorageService;
    private final GameService gameService;

    private final GameMapper gameMapper;
    private final GameEntryDtoFactory gameEntryDtoFactory;

    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String CREATE_GAME = "/api/games";
    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String UPDATE_GAME = "/api/games/{game_id}";
    public static final String FIND_GAME = "/api/games/{game_id}";
    public static final String DELETE_GAME = "/api/games/{game_id}";
    public static final String SEARCH_GAMES = "/api/games";

    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String CREATE_GAME_ENTRY = "/api/users/{username}/games";
    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    public static final String UPDATE_GAME_ENTRY = "/api/games/submissions/{submission_id}";
    public static final String DELETE_GAME_ENTRY = "/api/games/submissions/{submission_id}";
    public static final String FIND_GAME_ENTRIES = "/api/users/{username}/games";

    /**
     * Обрабатывает входящеий запрос на создание нового Game.
     * @param gameDto объект, содержащий необходимые для создания Game данные
     * @param image изображение, которое будет использоваться в качестве обложки игры
     * @return GameDto, содержащий данные созданной Game
     */
    @Operation(
            summary = "Create game",
            description = "Use to create new game"
    )
    @PostMapping(CREATE_GAME)
    @ResponseStatus(HttpStatus.CREATED)
    public GameDto createGame(@Valid @RequestPart("game") GameCreationDto gameDto,
                              @RequestPart(value = "image", required = false) MultipartFile image) {
        var gameEntity = gameService.createGame(gameDto);
        if (image != null) {
            var id = gameEntity.getId();
            imageStorageService.store(image, "games/%d".formatted(id));
        }
        return gameMapper.entityToDto(gameEntity);
    }

    /**
     * Game update
     * @param gameId Game id
     * @param gameDto Dto, from which data for update will be taken
     * @return updated or created game
     */
    @Operation(
            summary = "Update game",
            description = "Use to update existing game"
    )
    @PutMapping(UPDATE_GAME)
    public  GameDto putGame(@PathVariable(value = "game_id") Long gameId,
                            @Valid @RequestBody GameCreationDto gameDto) {
        return gameMapper.entityToDto(gameService.updateGame(gameId, gameDto));
    }


    /**
     * Find game by id
     * @param gameId Game id
     * @return Game with specified id
     * @throws NotFoundException in case there is no game with such id
     */
    @Operation(
            summary = "Find game",
            description = "Use to find game by id"
    )
    @GetMapping(FIND_GAME)
    public GameDto findGame(@PathVariable(value = "game_id") Long gameId) throws NotFoundException {
        return gameMapper.entityToDto(gameService.findGameOrElseThrowException(gameId));
    }

    /**
     * Delete game by id
     * @param gameId Game id
     * @throws NotFoundException in case there is no game with such id
     */
    @Operation(
            summary = "Delete game",
            description = "Use to delete game by id"
    )
    @DeleteMapping(DELETE_GAME)
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void deleteGame(@PathVariable("game_id") Long gameId) throws NotFoundException {
        gameService.deleteGame(gameId);
    }

    /**
     * Search games
     * @param title Title of the game
     * @return List of games that meet the conditions
     */
    @SuppressWarnings("checkstyle:ParameterNumber")
    @Operation(
            summary = "Search games",
            description = "Use to search games with specific properties"
    )
    @GetMapping(SEARCH_GAMES)
    public SearchResponseDto<GameDto> searchGames(
            @RequestParam(value = "title", required = false)
            @Parameter(description = "Title of the game") String title,

            @RequestParam(value = "time_from", required = false)
            @Parameter(description = "Minimum time") Long timeFrom,

            @RequestParam(value = "time_to", required = false)
            @Parameter(description = "Maximum time") Long timeTo,

            @RequestParam(value = "score_from", required = false)
            @Parameter(description = "The score is not lower than the specified") Float scoreFrom,

            @RequestParam(value = "score_to", required = false)
            @Parameter(description = "Score is not greater than the specified") Float scoreTo,

            @RequestParam(value = "release_after", required = false)
            @Parameter(description = "The release date is not earlier than the specified")
            LocalDate releaseAfter,

            @RequestParam(value = "release_before", required = false)
            @Parameter(description = "The release date is not later than the specified")
            LocalDate releaseBefore,

            @RequestParam(value = "game_type", required = false)
            @Parameter(description = "Type of the game")
            String gameType,

            @RequestParam(required = false, defaultValue = "0")
            @Validated @Min(0) Integer page,
            @RequestParam(value = "page_size", required = false, defaultValue = "10")
            @Validated @Min(0) @Max(100) Integer pageSize) {
        var searchResult = gameService.search(title,
                timeFrom, timeTo,
                scoreFrom, scoreTo,
                releaseAfter, releaseBefore,
                gameType,
                page, pageSize);
        return new SearchResponseDto<>(
                searchResult.getTotalPages(),
                searchResult.stream().map(gameMapper::entityToDto).toList()
        );
    }

    @Operation(
            summary = "Search game entries by user",
            description = "Use to search game entries by user "
    )
    @GetMapping(FIND_GAME_ENTRIES)
    public List<GameEntryResponseDto> findGameEntriesByUser(@PathVariable(name = "username") String username) {
        return gameService
                .findGameEntriesByUser(username)
                .stream()
                .map(gameEntryDtoFactory::entityToDto)
                .toList();
    }

    @Operation(
            summary = "Create new game entry for user",
            description = "Use to create game entries for users"
    )
    @PostMapping(CREATE_GAME_ENTRY)
    public GameEntryResponseDto createGameEntry(@PathVariable(name = "username") String username,
                                                @RequestBody GameEntryRequestDto gameEntryDto,
                                                Authentication authentication) {
        if (!authentication.getName().equals(username)) {
            throw new ForbiddenException("aa");
        }
        return gameEntryDtoFactory.entityToDto(gameService.createGameEntry(username, gameEntryDto));
    }

    @Operation(
            summary = "Update game entry",
            description = "Use to update game entries. Auth required"
    )
    @PutMapping(UPDATE_GAME_ENTRY)
    public void putGameEntry(@PathVariable(name = "submission_id") Long gameEntryId,
                             @RequestBody GameEntryRequestDto gameEntryDto,
                             Authentication authentication) {
        gameService.updateGameEntry(gameEntryId, gameEntryDto, authentication);
    }

    @Operation(
            summary = "Delete game entry",
            description = "Use to delete game entries. Auth required"
    )
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping(DELETE_GAME_ENTRY)
    public void deleteGameEntry(@PathVariable(name = "submission_id") Long gameEntryId,
                                Authentication authentication) throws NotFoundException {
        gameService.deleteGameEntry(gameEntryId, authentication);
    }

    @Hidden
    @GetMapping("/api/games-auto-create/title")
    public void autoCreate(@RequestParam(value = "title", required = false) String title) {
        gameService.autoCreateGame(title);
    }

    @Hidden
    @GetMapping("/api/games-auto-create/page")
    public void autoCreate(@RequestParam(value = "id", required = false) Integer id,
                           @RequestParam(value = "limit", required = false) Integer limit) {
        gameService.autoCreateGame(id, limit);
    }
}
