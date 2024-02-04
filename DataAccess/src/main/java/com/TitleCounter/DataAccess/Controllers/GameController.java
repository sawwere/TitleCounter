package com.TitleCounter.DataAccess.Controllers;
import com.TitleCounter.DataAccess.Models.Game;
import com.TitleCounter.DataAccess.Services.GameService;
import org.apache.coyote.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.ErrorResponse;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.ModelAndView;

@RestController
public class GameController
{
    @Autowired
    private GameService gameService;
    @GetMapping("/games")
    public String index()
    {
        StringBuilder sb = new StringBuilder();
        for (Game game : gameService.findAllTopics()){
            sb.append(game.getFixed_title()).append(" ");
        }
        return sb.toString();
    }

    @GetMapping("/games/{id}")
    public Game game(@PathVariable Long id)
    {
        Game game = gameService.findGame(id);
        System.out.println(game.getStatus());
        var mv = new ModelAndView();
        mv.addObject(game);
        return game;
    }

    @PostMapping("/games/{id}")
    public HttpStatus game(Game game)
    {
        gameService.saveGame(game);
        return HttpStatus.CREATED;
    }

    @ExceptionHandler(NullPointerException.class)
    public ErrorResponse handleException(NullPointerException e) {
        return ErrorResponse.create(e, HttpStatus.NOT_FOUND, e.getMessage());
    }

}
