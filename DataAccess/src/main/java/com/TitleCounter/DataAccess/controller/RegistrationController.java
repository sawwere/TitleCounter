package com.TitleCounter.DataAccess.controller;

import com.TitleCounter.DataAccess.service.UserService;
import com.TitleCounter.DataAccess.storage.entity.User;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.servlet.ModelAndView;

@RequiredArgsConstructor
@Controller
public class RegistrationController {
    private final UserService userService;

    @GetMapping("/")
    public String index() {
        return "index";
    }

    @GetMapping("/news")
    public String news() {
        System.out.println(888);
        return "news";
    }
    @GetMapping("/register")
    public ModelAndView registration() {
        ModelAndView mav = new ModelAndView("registration");
        mav.addObject("userForm", new User());
        System.out.println(666);
        return mav;
    }

    @PostMapping("/register")
    public String addUser(@ModelAttribute("userForm") @Valid User userForm, BindingResult bindingResult, Model model) {
        ModelAndView mav = new ModelAndView("registration");
        if (bindingResult.hasErrors()) {
            return mav.getViewName();
        }
        if (!userService.saveUser(userForm)){
            mav.addObject("usernameError", "Пользователь с таким именем уже существует");
            return mav.getViewName();
        }
        return "redirect:/";
    }
}
