package com.sawwere.titlecounter.backend.app.controller.view;

import com.sawwere.titlecounter.backend.app.service.UserService;
import com.sawwere.titlecounter.backend.app.dto.user.UserDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.Errors;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.context.request.WebRequest;
import org.springframework.web.servlet.ModelAndView;

import java.util.Optional;

@RequiredArgsConstructor
@Controller
public class UserController {
        public static final String CREATE_USER = "api/users";

    public static final String GET_USER = "/users/{username}";

    private final UserService userService;
    private final UserDtoFactory userDtoFactory;


//    @PostMapping(CREATE_USER)
//    private UserDto createUser(@RequestBody UserDto userDto) {
//        User userEntity = userDtoFactory.dtoToEntity(userDto);
//        userService.createUser(userEntity);
//        return userDtoFactory.entityToDto(userEntity);
//    }

    @GetMapping("/registration")
    public String showRegistrationForm(WebRequest request, Model model) {
        UserRegistrationDto userDto = new UserRegistrationDto();
        model.addAttribute("userForm", userDto);
        return "registration";
    }

    @PostMapping("/registration")
    public ModelAndView registerUserAccount(
            @ModelAttribute("user") @Valid UserRegistrationDto userRegistrationDto,
            HttpServletRequest request,
            Errors errors) throws ServletException {

        User registered = userService.createUser(userRegistrationDto);
        request.login(userRegistrationDto.getUsername(), userRegistrationDto.getPassword());

        return new ModelAndView("index");
    }

    @GetMapping(GET_USER)
    private ModelAndView getUser(@PathVariable(value = "username") String username) {
        ModelAndView mav = new ModelAndView("user");
        User user = userService.findUserByUsername(username);
        mav.addObject("user", user);
        return mav;
    }
}
