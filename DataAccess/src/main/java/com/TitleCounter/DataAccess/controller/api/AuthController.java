package com.TitleCounter.DataAccess.controller.api;

import com.TitleCounter.DataAccess.dto.user.UserDto;
import com.TitleCounter.DataAccess.dto.user.UserLoginDto;
import com.TitleCounter.DataAccess.exception.ApiBadCredentialsException;
import com.TitleCounter.DataAccess.service.GameService;
import com.TitleCounter.DataAccess.storage.entity.User;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.Cookie;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.security.web.authentication.logout.SecurityContextLogoutHandler;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.logging.Logger;

@RequiredArgsConstructor
@RestController
public class AuthController {
    private static final Logger logger =
            Logger.getLogger(GameService.class.getName());

    public static final String API_LOGIN = "/api/login";
    public static final String API_LOGOUT = "/api/logout";

    private final SecurityContextLogoutHandler logoutHandler = new SecurityContextLogoutHandler();

    @PostMapping(API_LOGIN)
    public UserDto login(@Valid @RequestBody UserLoginDto userLoginDto, HttpServletRequest request) {
        try {
            request.login(userLoginDto.getUsername(), userLoginDto.getPassword());
        } catch (ServletException e) {
            logger.info(e.getMessage());
            throw new ApiBadCredentialsException("Invalid username or password");
        }
        var auth = (Authentication) request.getUserPrincipal();
        var user = (User) auth.getPrincipal();
        return UserDto.builder()
                .id(user.getId())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();
    }

    @PostMapping(API_LOGOUT)
    public void logout(Authentication authentication, HttpServletRequest request, HttpServletResponse response) {
        logoutHandler.logout(request, response, authentication);

//        try {
//            request.logout();
//            request.getSession().invalidate();
//
//            Cookie toRemove = new Cookie("SESSION", "");
//            toRemove.setMaxAge(0);
//            response.addCookie(toRemove);
//
//        } catch (ServletException e) {
//            logger.info(e.getMessage());
//        }
    }
}
