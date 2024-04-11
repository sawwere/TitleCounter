package com.TitleCounter.DataAccess.controller.view;

import com.TitleCounter.DataAccess.dto.GameEntryDtoFactory;
import com.TitleCounter.DataAccess.service.GameService;
import com.TitleCounter.DataAccess.service.UserService;
import com.TitleCounter.DataAccess.storage.entity.User;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.servlet.ModelAndView;

@RequiredArgsConstructor
@Controller
public class GameViewController {
    public static final String GET_ALL_GAMES = "/games";
    public static final String GET_GAME = "/games/{game_id}";
    public static final String SUBMIT_GAME = "/games/{game_id}/submit";
    public static final String GET_USER_GAMES = "/users/{username}/games";

    private final GameEntryDtoFactory gameEntryDtoFactory;

    private final GameService gameService;
    private final UserService userService;

    @GetMapping(GET_ALL_GAMES)
    private ModelAndView getAllGames() {
        ModelAndView mav = new ModelAndView("games");
        var games = gameService.findAll();
        mav.addObject("games", games);
        return mav;
    }

    @GetMapping(GET_GAME)
    private ModelAndView getGame(@PathVariable(value = "game_id") Long gameId) {
        ModelAndView mav = new ModelAndView("game");
        var game = gameService.findGame(1L).get();
        mav.addObject("game", game);
        return mav;
    }

    @GetMapping(SUBMIT_GAME)
    private ModelAndView submitGame(
            @PathVariable(value = "game_id") Long gameId,
            Authentication authentication) {
        ModelAndView mav = new ModelAndView("news");
        String username = authentication.getName();
        User user = userService.findUserByUsername(username);
        gameService.createGameEntry(username, gameEntryDtoFactory.makeDefault(gameId, user.getId()));
        return mav;
    }



    @GetMapping(GET_USER_GAMES)
    private ModelAndView getUserGames(@PathVariable(value = "username") String username) {
        ModelAndView mav = new ModelAndView("user_games");
        User user = userService.findUserByUsername(username);
        var games = gameService.findGamesByUser(username);
        System.out.println(games.size());
        mav.addObject("games", games);
        return mav;
    }
}
