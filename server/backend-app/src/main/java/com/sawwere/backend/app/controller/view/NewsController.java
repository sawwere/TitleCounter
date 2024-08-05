package com.sawwere.backend.app.controller.view;

import com.sawwere.backend.app.storage.entity.User;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
public class NewsController {

    @GetMapping("/news")
    private ModelAndView news(Authentication authentication) {
        ModelAndView mav = new ModelAndView("news");
        User user = (User) authentication.getPrincipal();
        System.out.println(user.getUsername());
        System.out.println(user.getRoles().stream().anyMatch(x->x.getName().equals("ADMIN")));
        return mav;
    }
}
