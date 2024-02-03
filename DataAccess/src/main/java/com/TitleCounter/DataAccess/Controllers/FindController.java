package com.TitleCounter.DataAccess.Controllers;

import com.TitleCounter.DataAccess.Models.Game;
import com.TitleCounter.DataAccess.Services.GameService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class FindController {
    @Autowired
    private GameService gameService;
    @GetMapping("/find")
    public String index(@RequestParam String mode, @RequestParam String title) {
        StringBuilder sb = new StringBuilder();
        for (Game game : gameService.findAllTopics()){
            sb.append(game.getFixed_title()).append(" ");
        }
        return sb.toString();
    }
}
