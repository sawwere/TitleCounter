package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.JwtAuthenticationResponse;
import com.sawwere.titlecounter.backend.app.dto.user.UserDto;
import com.sawwere.titlecounter.backend.app.dto.user.UserLoginDto;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.exception.ApiBadCredentialsException;
import com.sawwere.titlecounter.backend.app.service.AuthService;
import com.sawwere.titlecounter.backend.app.service.GameService;
import com.sawwere.titlecounter.backend.app.service.JwtService;
import com.sawwere.titlecounter.backend.app.service.UserService;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.validation.Valid;
import jakarta.ws.rs.NotSupportedException;
import lombok.RequiredArgsConstructor;
import org.apache.catalina.Authenticator;
import org.apache.commons.lang.NotImplementedException;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.authentication.logout.SecurityContextLogoutHandler;
import org.springframework.web.bind.annotation.*;

import java.util.logging.Logger;
import java.util.stream.Collectors;

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

    @GetMapping(REFRESH_TOKEN)
    public JwtAuthenticationResponse refreshToken(HttpServletRequest request) {
        return authService.refreshToken(request);
    }
}
