package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.game.*;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.service.GameService;
import com.sawwere.titlecounter.backend.app.service.ImageStorageService;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryRequestDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryResponseDto;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping(produces="application/json")
@RequiredArgsConstructor
public class GameController
{
    private final ImageStorageService imageStorageService;
    private final GameService gameService;

    private final GameDtoFactory gameDtoFactory;
    private final GameEntryDtoFactory gameEntryDtoFactory;

    public static final String CREATE_GAME = "/api/games";
    public static final String UPDATE_GAME = "/api/games/{game_id}";
    public static final String FIND_GAME = "/api/games/{game_id}";
    public static final String DELETE_GAME = "/api/games/{game_id}";
    public static final String FIND_ALL_GAMES = "/api/games";

    public static final String CREATE_GAME_ENTRY = "/api/users/{username}/games";
    public static final String UPDATE_GAME_ENTRY = "/api/games/submissions/{submission_id}";
    public static final String DELETE_GAME_ENTRY = "/api/games/submissions/{submission_id}";
    public static final String FIND_GAME_ENTRIES = "/api/users/{username}/games";

    /**
     * Обрабатывает входящеий запрос на создание нового Game.
     * @param gameDto объект, содержащий необходимые для создания Game данные
     * @return GameDto, содержащий данные созданной Game
     */
    @PostMapping(CREATE_GAME)
    @ResponseStatus(HttpStatus.CREATED)
    public GameDto createGame(@Valid @RequestPart("game") GameDto gameDto,
                              @RequestPart("image") MultipartFile image) {
        var gameEntity = gameService.createGame(gameDto);
        var id = gameEntity.getId();
        imageStorageService.store(image, "games/%d".formatted(id));
        return gameDtoFactory.entityToDto(gameEntity);
    }

    @PutMapping(UPDATE_GAME)
    public  GameDto putGame(@PathVariable(value = "game_id") Long gameId,
                            @Valid @RequestBody GameDto gameDto) {
        return gameDtoFactory.entityToDto(gameService.updateGame(gameId, gameDto));
    }

    @GetMapping(FIND_GAME)
    public GameDto findGame(@PathVariable(value = "game_id") Long gameId) {
        return gameDtoFactory.entityToDto(gameService.findGameOrElseThrowException(gameId));
    }

    @DeleteMapping(DELETE_GAME)
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void deleteGame(@PathVariable("game_id") Long gameId) {
        gameService.deleteGame(gameId);
    }

    @GetMapping(FIND_ALL_GAMES)
    public List<GameDto> findAllGames(@RequestParam(value = "q", required = false) Optional<String> query) {
        return gameService.search(query).stream().map(gameDtoFactory::entityToDto).toList();
    }

    @GetMapping(FIND_GAME_ENTRIES)
    public List<GameEntryResponseDto> findGameEntriesByUser(@PathVariable(name="username") String username) {
        return gameService
                .findGameEntriesByUser(username)
                .stream()
                .map(gameEntryDtoFactory::entityToDto)
                .toList();
    }

    @PostMapping(CREATE_GAME_ENTRY)
    public GameEntryResponseDto createGameEntry(@PathVariable(name="username") String username,
                                                @RequestBody GameEntryRequestDto gameEntryDto,
                                                Authentication authentication) {
        if (!authentication.getName().equals(username))
        {
            throw new ForbiddenException("aa");
        }
        return gameEntryDtoFactory.entityToDto(gameService.createGameEntry(username, gameEntryDto));
    }

    @PutMapping(UPDATE_GAME_ENTRY)
    public void putGameEntry(@PathVariable(name="submission_id") Long gameEntryId,
                             @RequestBody GameEntryRequestDto gameEntryDto,
                             Authentication authentication) {
        gameService.updateGameEntry(gameEntryId, gameEntryDto, authentication);
    }

    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping(DELETE_GAME_ENTRY)
    public void createGameEntry(@PathVariable(name="submission_id") Long gameEntryId,
                                Authentication authentication) {
        gameService.deleteGameEntry(gameEntryId, authentication);
    }
}
