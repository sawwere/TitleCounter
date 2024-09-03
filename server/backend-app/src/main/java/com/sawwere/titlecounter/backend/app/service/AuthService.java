package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.JwtAuthenticationResponse;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.common.dto.user.UserLoginDto;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.exception.ApiBadCredentialsException;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import jakarta.servlet.http.HttpServletRequest;
import lombok.RequiredArgsConstructor;
import org.apache.commons.lang.NotImplementedException;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.stereotype.Service;

import static com.sawwere.titlecounter.backend.app.controller.api.AuthController.API_LOGIN;
import static org.springframework.http.HttpHeaders.AUTHORIZATION;

@Service
@RequiredArgsConstructor
public class AuthService {
    private final AuthenticationManager authenticationManager;

    private final JwtService jwtService;
    private final UserService userService;
    @Value("${token.accessExpirationTimeout}")
    private int accessExpirationTimeout;

    public JwtAuthenticationResponse login(UserLoginDto userLoginDto) {
        authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(
                userLoginDto.getUsername(),
                userLoginDto.getPassword()
        ));

        try {
            var user = userService
                    .findUserByUsername(userLoginDto.getUsername());
            String accessToken = jwtService.createAccessToken(user, API_LOGIN);
            String refreshToken = jwtService.createRefreshToken(user.getUsername());
            return new JwtAuthenticationResponse(accessToken, refreshToken, accessExpirationTimeout * 60);
        }
        catch (NotFoundException ex) {
            throw new ApiBadCredentialsException("Invalid credentials given");
        }
    }

    public JwtAuthenticationResponse register(UserRegistrationDto userRegistrationDto) {
        throw new NotImplementedException();
    }

    public JwtAuthenticationResponse refreshToken(HttpServletRequest request) {
        String authorizationHeader = request.getHeader(AUTHORIZATION);
        if(authorizationHeader == null || !authorizationHeader.startsWith("Bearer ")) {
            throw new RuntimeException("Token is missing");
        }

        String refreshToken = request.getHeader(AUTHORIZATION).substring("Bearer ".length());
        try {
            UsernamePasswordAuthenticationToken authenticationToken = jwtService.parseToken(refreshToken);
            User user = userService.findUserByUsername(authenticationToken.getName());
            String accessToken = jwtService.createAccessToken(
                    user,
                    request.getRequestURL().toString()
            );
            return new JwtAuthenticationResponse(accessToken, refreshToken, accessExpirationTimeout);
        }
        catch (Exception e) {
            throw new ApiBadCredentialsException("Invalid jwt was given");
        }

    }
}
