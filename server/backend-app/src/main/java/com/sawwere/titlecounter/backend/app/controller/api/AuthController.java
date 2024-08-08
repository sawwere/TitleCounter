package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.JwtAuthenticationResponse;
import com.sawwere.titlecounter.backend.app.dto.user.UserLoginDto;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.service.AuthService;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.security.web.authentication.logout.SecurityContextLogoutHandler;
import org.springframework.web.bind.annotation.*;

@RequiredArgsConstructor
@RestController()
public class AuthController {
    public static final String API_LOGIN = "/api/auth/login";
    public static final String API_REGISTER = "/api/auth/register";
    public static final String API_LOGOUT = "/api/auth/logout";
    public static final String REFRESH_TOKEN = "/api/auth/token";

    private final SecurityContextLogoutHandler logoutHandler = new SecurityContextLogoutHandler();

    private final AuthService authService;

    @PostMapping(API_LOGIN)
    public JwtAuthenticationResponse login(@Valid @RequestBody UserLoginDto userLoginDto) {
        return authService.login(userLoginDto);
    }

    @PostMapping(API_REGISTER)
    public JwtAuthenticationResponse register(@Valid @RequestBody UserRegistrationDto userRegistrationDto) {
        return authService.register(userRegistrationDto);
    }

    @PostMapping(API_LOGOUT)
    public void logout(Authentication authentication, HttpServletRequest request, HttpServletResponse response) {
        logoutHandler.logout(request, response, authentication);
    }

    @GetMapping(REFRESH_TOKEN)
    public JwtAuthenticationResponse refreshToken(HttpServletRequest request) {
        return authService.refreshToken(request);
    }
}
