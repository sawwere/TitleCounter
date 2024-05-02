package com.TitleCounter.DataAccess.config;

import com.TitleCounter.DataAccess.controller.api.AuthController;
import com.TitleCounter.DataAccess.controller.api.FilmController;
import com.TitleCounter.DataAccess.controller.api.GameController;
import com.TitleCounter.DataAccess.service.UserService;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.HttpStatusEntryPoint;
import org.springframework.security.web.authentication.logout.HttpStatusReturningLogoutSuccessHandler;
import org.springframework.security.web.util.matcher.RequestMatcher;

import java.util.Optional;


@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class WebSecurityConfig {
    private final UserService userService;
    @Bean
    public UserDetailsService userDetailsService(UserRepository userRepo) {
        return username -> {
            Optional<User> user = userRepo.findByUsername(username);
            if (user.isPresent())
                return user.get();
            else
                throw new UsernameNotFoundException("User ‘" + username + "’ not found");
        };
    }
    @Bean
    public RequestMatcher apiHttpGetRequestMatcher() {
        return request -> {
            String requestUri = request.getRequestURI();
            return request.getMethod().equals(HttpMethod.GET.toString())
                    && (requestUri.equals(GameController.FIND_GAME)
                        || requestUri.equals(GameController.FIND_ALL_GAMES)
                        || requestUri.equals(GameController.FIND_GAME_ENTRIES)
                        || requestUri.equals(FilmController.FIND_FILM)
                        || requestUri.equals(FilmController.FIND_ALL_FILMS)
                        || requestUri.equals(FilmController.FIND_FILM_ENTRIES))
                    ;
        };
    }

    @Bean
    public BCryptPasswordEncoder bCryptPasswordEncoder() {
        return new BCryptPasswordEncoder();
    }
    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http
                .csrf(AbstractHttpConfigurer::disable)
                .authorizeHttpRequests((auth) -> auth
                        .requestMatchers(AuthController.API_LOGIN).anonymous()
                        .requestMatchers(AuthController.API_LOGOUT).authenticated()
                        .requestMatchers(
                                HttpMethod.GET,
                                GameController.FIND_GAME, GameController.FIND_ALL_GAMES, GameController.FIND_GAME_ENTRIES,
                                FilmController.FIND_FILM, FilmController.FIND_ALL_FILMS, FilmController.FIND_FILM_ENTRIES
                        ).permitAll()
                        .requestMatchers("/", "/error",
                                "/login", "/registration",
                                "/games/*", "/games", "/users/**",
                                "/css/**", "/images/**").permitAll()
                        .requestMatchers("/games/*/submit").authenticated()
                        .requestMatchers(
                                HttpMethod.POST,
                                GameController.CREATE_GAME, FilmController.CREATE_FILM
                        ).hasAuthority("ADMIN")
                        .requestMatchers(
                                HttpMethod.PUT,
                                GameController.UPDATE_GAME, FilmController.UPDATE_FILM
                        ).hasAuthority("ADMIN")
                        .requestMatchers(
                                HttpMethod.DELETE,
                                GameController.DELETE_GAME, FilmController.DELETE_FILM
                        ).hasAuthority("ADMIN")
                        .requestMatchers(
                                HttpMethod.POST,
                                GameController.CREATE_GAME_ENTRY, FilmController.CREATE_FILM_ENTRY
                        ).authenticated()
                        .requestMatchers(
                                HttpMethod.PUT,
                                GameController.UPDATE_GAME_ENTRY, FilmController.UPDATE_FILM_ENTRY
                        ).authenticated()
                        .requestMatchers(
                                HttpMethod.DELETE,
                                GameController.DELETE_GAME_ENTRY, FilmController.DELETE_FILM_ENTRY
                        ).authenticated()
                        .anyRequest().authenticated()
                )
                .sessionManagement(session -> session
                        .invalidSessionUrl("/error")
                        .maximumSessions(5))
                .formLogin(form -> form
                        .defaultSuccessUrl("/")
                        .permitAll())
                .logout(logout -> logout
                        .invalidateHttpSession(true)
                        .deleteCookies("JSESSIONID", "SESSION")
                        .logoutSuccessHandler(new HttpStatusReturningLogoutSuccessHandler()))
                .rememberMe(x->x
                       .rememberMeParameter("remember-me-new")
                        .alwaysRemember(false)
                        .userDetailsService(userService)
                        .tokenValiditySeconds(60*60*24))
                .exceptionHandling(ex -> ex.authenticationEntryPoint(new HttpStatusEntryPoint(HttpStatus.UNAUTHORIZED)))
                //.oauth2ResourceServer(oath2 -> oath2.jwt(withDefaults()))
                ;
        return http.build();
    }

    @Autowired
    void configure(AuthenticationManagerBuilder builder) throws Exception {
        builder.userDetailsService(userService).passwordEncoder(new BCryptPasswordEncoder());
    }
}
