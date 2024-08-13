package com.sawwere.titlecounter.frontend.app.controller;

import com.sawwere.titlecounter.frontend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.frontend.app.service.ApiClient;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.apache.commons.lang.NotImplementedException;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.Errors;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.context.request.WebRequest;
import org.springframework.web.servlet.ModelAndView;

@RequiredArgsConstructor
@Controller
public class UserController {
    public static final String LOGIN = "/auth/login";
    public static final String REGISTRATION = "/auth/register";
    public static final String LOGOUT = "/auth/logout";
    public static final String GET_USER = "/users/{username}";

    private final ApiClient apiClient;

    @GetMapping(REGISTRATION)
    public String getRegistration(WebRequest request, Model model) {
        UserRegistrationDto userDto = new UserRegistrationDto();
        model.addAttribute("userForm", userDto);
        return "registration";
    }

    @PostMapping(REGISTRATION)
    public ModelAndView postRegistration(
            @ModelAttribute("user") @Valid UserRegistrationDto userRegistrationDto,
            HttpServletRequest request,
            Errors errors) throws ServletException {
        request.login(userRegistrationDto.getUsername(), userRegistrationDto.getPassword());

        return new ModelAndView("index");
    }

    @GetMapping(LOGIN)
    public String getLogin(WebRequest request, Model model) {
        UserRegistrationDto userDto = new UserRegistrationDto();
        model.addAttribute("userForm", userDto);
        return "registration";
    }

    @GetMapping(GET_USER)
    private ModelAndView getUser(@PathVariable(value = "username") String username) {
        throw new NotImplementedException(GET_USER);
    }
}
