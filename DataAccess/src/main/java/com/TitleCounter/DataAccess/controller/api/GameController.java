package com.TitleCounter.DataAccess.controller.api;
import com.TitleCounter.DataAccess.dto.GameDto;
import com.TitleCounter.DataAccess.dto.GameDtoFactory;
import com.TitleCounter.DataAccess.dto.GameEntryDto;
import com.TitleCounter.DataAccess.dto.GameEntryDtoFactory;
import com.TitleCounter.DataAccess.service.GameService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping(produces="application/json")
@RequiredArgsConstructor
public class GameController
{
    private final GameService gameService;

    private final GameDtoFactory gameDtoFactory;
    private final GameEntryDtoFactory gameEntryDtoFactory;

    public static final String CREATE_GAME = "/api/games";
    public static final String UPDATE_GAME = "/api/games/{game_id}";
    public static final String FIND_GAME = "/api/games/{game_id}";
    public static final String DELETE_GAME = "/api/games/{game_id}";
    public static final String FIND_ALL_GAMES = "/api/games";

    public static final String CREATE_GAME_ENTRY = "/api/users/{username}/games";
    public static final String DELETE_GAME_ENTRY = "/api/submissions/{submission_id}";
    public static final String FIND_GAME_ENTRIES = "/api/users/{username}/games";

    /**
     * Обрабатывает входящеий запрос на создание нового Game.
     * @param gameDto объект, содержащий необходимые для создания Game данные
     * @return GameDto, содержащий данные созданной Game
     */
    @PostMapping(CREATE_GAME)
    @ResponseStatus(HttpStatus.CREATED)
    public GameDto createGame(@Valid @RequestBody GameDto gameDto) {
        return gameDtoFactory.entityToDto(gameService.createGame(gameDto));
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
    public List<GameDto> findAllGames() {
        return gameService.findAll().stream().map(gameDtoFactory::entityToDto).collect(Collectors.toList());
    }



    @GetMapping(FIND_GAME_ENTRIES)
    public List<GameEntryDto> findGameEntriesByUser(@PathVariable(name="username") String username) {
        return gameService.findGameEntriesByUser(username).map(gameEntryDtoFactory::entityToDto).collect(Collectors.toList());
    }

    @PostMapping(CREATE_GAME_ENTRY)
    public GameEntryDto createGameEntry(@PathVariable(name="username") String username,
                                        @RequestBody GameEntryDto gameEntryDto) {
        return gameEntryDtoFactory.entityToDto(gameService.createGameEntry(username, gameEntryDto));
    }

    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping(DELETE_GAME_ENTRY)
    public void createGameEntry(@PathVariable(name="submission_id") Long gameEntryId) {
        gameService.deleteGameEntry(gameEntryId);
    }
}
