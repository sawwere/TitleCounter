package com.sawwere.titlecounter.frontend.app.controller;

import org.apache.commons.lang.NotImplementedException;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
public class NewsController {

    @GetMapping("/news")
    private ModelAndView news(Authentication authentication) {
        throw new NotImplementedException("NEWS");
    }
}
