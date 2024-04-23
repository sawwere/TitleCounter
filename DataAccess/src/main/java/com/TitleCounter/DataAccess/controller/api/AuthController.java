package com.TitleCounter.DataAccess.controller.api;

import com.TitleCounter.DataAccess.dto.UserLoginDto;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RequiredArgsConstructor
@RestController
public class AuthController {
    //private final AuthenticationManager authenticationManager;
    @PostMapping("/api/login")
    public void login(@Valid @RequestBody UserLoginDto userLoginDto, HttpServletRequest request) {
        Authentication authenticationRequest =
                UsernamePasswordAuthenticationToken.unauthenticated(userLoginDto.getUsername(), userLoginDto.getPassword());
        //request.login(userLoginDto.getUsername(), userLoginDto.getPassword());
        try {
            request.login(userLoginDto.getUsername(), userLoginDto.getPassword());
        } catch (ServletException e) {
            System.out.println(e.getMessage());
            System.out.println(e.getCause());
            throw new RuntimeException("Invalid username or password");
        }
    }
}
