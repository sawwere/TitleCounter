package com.TitleCounter.DataAccess.controller;

import com.TitleCounter.DataAccess.storage.entity.User;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.ModelAndView;

@RestController
public class FindController {
//    @Autowired
//    private GameService gameService;
//    @GetMapping("/find")
//    public String index(@RequestParam String mode, @RequestParam String title) {
//        StringBuilder sb = new StringBuilder();
//        for (Game game : gameService.findAllTopics()){
//            sb.append(game.getFixed_title()).append(" ");
//        }
//        return sb.toString();
//    }
    @GetMapping("/news")
    private ModelAndView news(Authentication authentication) {
        ModelAndView mav = new ModelAndView("news");
        User user = (User) authentication.getPrincipal();
        System.out.println(user.getUsername());
        return mav;
    }
}
