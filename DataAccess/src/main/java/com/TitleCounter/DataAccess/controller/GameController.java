package com.TitleCounter.DataAccess.controller;
import com.TitleCounter.DataAccess.dto.GameDto;
import com.TitleCounter.DataAccess.dto.GameDtoFactory;
import com.TitleCounter.DataAccess.service.GameService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequiredArgsConstructor
public class GameController
{
    private final GameService gameService;
    private final GameDtoFactory gameDtoFactory;

    public static final String CREATE_GAME = "/api/games";
    public static final String FIND_GAME = "/api/games/{game_id}";
    public static final String DELETE_GAME = "/api/games/{game_id}";
    public static final String FIND_ALL_GAMES = "/api/games";

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

    @GetMapping(FIND_GAME)
    public GameDto findClient(@PathVariable(value = "game_id") Long gameId) {
        return gameDtoFactory.entityToDto(gameService.findGameOrElseThrowException(gameId));
    }

    @DeleteMapping(DELETE_GAME)
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void deleteClient(@PathVariable("game_id") Long gameId) {
        gameService.deleteGame(gameId);
    }

    /**
     * Обрабатывает входящеий запрос на получение списка всех существующих в базе данных клиентов.
     * @return список всех клиентов
     */
    @GetMapping(FIND_ALL_GAMES)
    public List<GameDto> findAllGames() {
        return gameService.findAll().stream().map(gameDtoFactory::entityToDto).collect(Collectors.toList());
    }
}
