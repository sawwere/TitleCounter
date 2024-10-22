package com.sawwere.titlecounter.frontend.app.controller;

import com.sawwere.titlecounter.frontend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.frontend.app.service.ApiClient;
import lombok.RequiredArgsConstructor;
import org.apache.commons.lang.NotImplementedException;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.servlet.ModelAndView;

@RequiredArgsConstructor
@Controller
public class GameController {
    public static final String GET_ALL_GAMES = "/games";
    public static final String GET_GAME = "/games/{game_id}";
    public static final String CREATE_GAME = "/games/new";
    public static final String EDIT_GAME = "/games/{game_id}/edit";
    public static final String SUBMIT_GAME_ENTRY = "/games/{game_id}/submit";
    public static final String GET_USER_GAME_ENTRIES = "/users/{username}/games";

    private final ApiClient apiClient;

    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    @GetMapping(GET_ALL_GAMES)
    private ModelAndView getAllGames() {
        ModelAndView mav = new ModelAndView("games");
        var games = apiClient.findAllGames(null);
        mav.addObject("games", games);
        return mav;
    }

    @SuppressWarnings("checkstyle:MultipleStringLiterals")
    @GetMapping(GET_GAME)
    private ModelAndView getGame(@PathVariable(value = "game_id") Long gameId) {
        ModelAndView mav = new ModelAndView("game");
        var game = apiClient.findGame(gameId);
        mav.addObject("game", game);
        return mav;
    }

    @GetMapping(CREATE_GAME)
    private ModelAndView getGame() {
        ModelAndView mav = new ModelAndView("game_create");
        mav.addObject("newGame", new GameCreationDto());
        return mav;
    }

    @PostMapping(CREATE_GAME)
    private String createGame(@ModelAttribute GameCreationDto game) {
        throw new NotImplementedException(CREATE_GAME);
    }

    @PostMapping(EDIT_GAME)
    private String editGame(@PathVariable (value = "game_id") Long gameId,
            @ModelAttribute GameCreationDto game) {
        throw new NotImplementedException(EDIT_GAME);
    }

    @GetMapping(SUBMIT_GAME_ENTRY)
    private ModelAndView submitGame(
            @PathVariable(value = "game_id") Long gameId,
            Authentication authentication) {
        throw new NotImplementedException(SUBMIT_GAME_ENTRY);
    }

    @GetMapping(GET_USER_GAME_ENTRIES)
    private ModelAndView getUserGames(@PathVariable(value = "username") String username) {
        ModelAndView mav = new ModelAndView("user_games");
        var games = apiClient.findGameEntriesByUser(username);
        mav.addObject("games", games);
        return mav;
    }
}
