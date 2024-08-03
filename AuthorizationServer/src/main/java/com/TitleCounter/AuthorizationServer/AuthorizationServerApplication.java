package com.TitleCounter.AuthorizationServer;

import com.TitleCounter.AuthorizationServer.storage.entity.User;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.validation.Valid;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
@RestController
public class AuthorizationServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(AuthorizationServerApplication.class, args);
	}

	public static final String API_LOGIN = "/api/login";
	@PostMapping(API_LOGIN)
	public UserDto login(@Valid @RequestBody UserLoginDto userLoginDto, HttpServletRequest request) {
		try {
			request.login(userLoginDto.getUsername(), userLoginDto.getPassword());
		} catch (ServletException e) {
			System.out.println(e.getMessage());
		}
		var auth = (Authentication) request.getUserPrincipal();
		var user = (User) auth.getPrincipal();
		return UserDto.builder()
				.id(user.getId())
				.username(user.getUsername())
				.email(user.getEmail())
				.build();
	}

}
