package com.sawwere.titlecounter.backend.app.controller.view;

import com.sawwere.titlecounter.backend.app.service.GameService;
import com.sawwere.titlecounter.backend.app.service.UserService;
import com.sawwere.titlecounter.backend.app.dto.GameCreationDto;
import com.sawwere.titlecounter.backend.app.dto.GameDto;
import com.sawwere.titlecounter.backend.app.dto.GameEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.service.ImageStorageService;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.servlet.ModelAndView;

@RequiredArgsConstructor
@Controller
public class GameViewController {
    public static final String GET_ALL_GAMES = "/games";
    public static final String GET_GAME = "/games/{game_id}";
    public static final String CREATE_GAME = "/games/new";
    public static final String EDIT_GAME = "/games/{game_id}/edit";
    public static final String SUBMIT_GAME = "/games/{game_id}/submit";
    public static final String GET_USER_GAMES = "/users/{username}/games";

    private final GameEntryDtoFactory gameEntryDtoFactory;

    private final ImageStorageService imageStorageService;
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
        var game = gameService.findGameOrElseThrowException(gameId);
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
        GameDto gameDto = GameDto.builder()
                .title(game.getTitle())
                .time(game.getTime())
                .globalScore(game.getGlobalScore())
                .dateRelease(game.getDateRelease())
                .linkUrl(game.getLinkUrl())
                .build();
        var id = gameService.createGame(gameDto).getId();
        var image = game.getImage();
        imageStorageService.store(image, "games/%d".formatted(id));
        return GET_ALL_GAMES;
    }

    @PostMapping(EDIT_GAME)
    private String editGame(@PathVariable (value = "game_id") Long gameId,
            @ModelAttribute GameCreationDto game) {
        GameDto gameDto = GameDto.builder()
                .title(game.getTitle())
                .time(game.getTime())
                .globalScore(game.getGlobalScore())
                .dateRelease(game.getDateRelease())
                .linkUrl(game.getLinkUrl())
                .build();
        var id = gameService.updateGame(gameId, gameDto).getId();
        var image = game.getImage();
        imageStorageService.store(image, "games/%d".formatted(id));
        return GET_ALL_GAMES;
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
