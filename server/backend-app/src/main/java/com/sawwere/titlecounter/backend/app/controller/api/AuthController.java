package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.JwtAuthenticationResponse;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.service.AuthService;
import com.sawwere.titlecounter.common.dto.user.UserLoginDto;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.security.web.authentication.logout.SecurityContextLogoutHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RequiredArgsConstructor
@RestController()
public class AuthController {
    public static final String API_LOGIN = "/api/auth/login";
    public static final String API_REGISTER = "/api/auth/register";
    public static final String API_REGISTER_RESEND_LINK = "/api/auth/register/resend";
    public static final String API_CONFIRM_REGISTER = "/api/auth/register/confirm";
    public static final String API_LOGOUT = "/api/auth/logout";
    public static final String REFRESH_TOKEN = "/api/auth/token";

    private final SecurityContextLogoutHandler logoutHandler = new SecurityContextLogoutHandler();

    private final AuthService authService;

    @PostMapping(API_LOGIN)
    public JwtAuthenticationResponse login(@Valid @RequestBody UserLoginDto userLoginDto) {
        return authService.login(userLoginDto);
    }

    @PostMapping(API_REGISTER)
    public void register(@Valid @RequestBody UserRegistrationDto userRegistrationDto) {
        authService.register(userRegistrationDto);
    }

    @GetMapping(API_REGISTER_RESEND_LINK)
    public void resendRegister(@Valid @RequestBody UserRegistrationDto userRegistrationDto) {
        authService.resendRegister(userRegistrationDto);
    }

    @GetMapping(API_CONFIRM_REGISTER)
    public void confirmRegister(@RequestParam String token) {
        authService.confirmRegister(token);
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
