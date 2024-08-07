package com.sawwere.titlecounter.backend.app.config;

import com.sawwere.titlecounter.backend.app.controller.api.GameController;
import com.sawwere.titlecounter.backend.app.controller.api.UserController;
import com.sawwere.titlecounter.backend.app.service.JwtService;
import com.sawwere.titlecounter.backend.app.service.UserService;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.UserRepository;
import com.sawwere.titlecounter.backend.app.controller.api.AuthController;
import com.sawwere.titlecounter.backend.app.controller.api.FilmController;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.HttpStatusEntryPoint;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;
import org.springframework.security.web.authentication.logout.HttpStatusReturningLogoutSuccessHandler;
import org.springframework.security.web.util.matcher.RequestMatcher;
import org.springframework.web.cors.CorsConfiguration;

import java.util.List;
import java.util.Optional;

import static com.sawwere.titlecounter.backend.app.controller.api.AuthController.*;

//import static com.sawwere.titlecounter.backend.app.controller.api.AuthController.AUTH;


@Configuration
@EnableWebSecurity
@EnableMethodSecurity
@RequiredArgsConstructor
public class WebSecurityConfig {
    private final JwtAuthenticationFilter jwtAuthenticationFilter;
    private final UserService userService;
    private final JwtService jwtService;

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
    public SecurityFilterChain filterChain(HttpSecurity http, AuthenticationManager authenticationManager) throws Exception {
        http
                .csrf(AbstractHttpConfigurer::disable)
                .cors(cors -> cors.configurationSource(request -> {
                    var corsConfiguration = new CorsConfiguration();
                    corsConfiguration.setAllowedOriginPatterns(List.of("*"));
                    corsConfiguration.setAllowedMethods(List.of("GET", "POST", "PUT", "DELETE", "OPTIONS"));
                    corsConfiguration.setAllowedHeaders(List.of("*"));
                    corsConfiguration.setAllowCredentials(true);
                    return corsConfiguration;
                }))
                .authorizeHttpRequests((auth) -> auth
                        .requestMatchers(AuthController.API_LOGIN, API_REGISTER).anonymous()
                        .requestMatchers(AuthController.API_LOGOUT).authenticated()
                        .requestMatchers(
                                HttpMethod.GET, GameController.FIND_GAME,
                                GameController.FIND_ALL_GAMES, GameController.FIND_GAME_ENTRIES,
                                FilmController.FIND_FILM, FilmController.FIND_ALL_FILMS, FilmController.FIND_FILM_ENTRIES
                        ).permitAll()
                        .requestMatchers("/", "/error","/users", REFRESH_TOKEN,
                                "/login", "/registration",
                                "/games/*", "/games", "/users/**",
                                "/css/**", "/images/**").permitAll()
                        .requestMatchers("/games/*/submit").authenticated()
                        .requestMatchers(UserController.ADD_ROLE).hasAuthority("ADMIN")
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
//                .sessionManagement(session -> session
//                        .invalidSessionUrl("/error")
//                        .maximumSessions(5))
                .sessionManagement(manager -> manager.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
                .authenticationProvider(authenticationProvider())
                .addFilter(customAuthenticationFilter(authenticationManager))
                .addFilterBefore(jwtAuthenticationFilter, UsernamePasswordAuthenticationFilter.class)
//                .formLogin(form -> form
//                        .defaultSuccessUrl("/")
//                        .permitAll())
//                .logout(logout -> logout
//                        .invalidateHttpSession(true)
//                        .deleteCookies("JSESSIONID", "SESSION")
//                        .logoutSuccessHandler(new HttpStatusReturningLogoutSuccessHandler()))
//                .rememberMe(x->x
//                       .rememberMeParameter("remember-me-new")
//                        .alwaysRemember(false)
//                        .userDetailsService(userService)
//                        .tokenValiditySeconds(60*60*24))
                //.exceptionHandling(ex -> ex.authenticationEntryPoint(new HttpStatusEntryPoint(HttpStatus.UNAUTHORIZED)))
                //.oauth2ResourceServer(oath2 -> oath2.jwt(withDefaults()))
                ;
        return http.build();
    }

    @Bean
    public CustomAuthenticationFilter customAuthenticationFilter(AuthenticationManager authenticationManager) {
        var res = new CustomAuthenticationFilter(jwtService);
        res.setAuthenticationManager(authenticationManager);
        return  res;
    }


    @Bean
    public BCryptPasswordEncoder bCryptPasswordEncoder() {
        return new BCryptPasswordEncoder();
    }
    @Bean
    public AuthenticationProvider authenticationProvider() {
        DaoAuthenticationProvider authProvider = new DaoAuthenticationProvider();
        authProvider.setUserDetailsService(userService);
        authProvider.setPasswordEncoder(bCryptPasswordEncoder());
        return authProvider;
    }

    @Bean
    public AuthenticationManager authenticationManager(AuthenticationConfiguration config)
            throws Exception {
        return config.getAuthenticationManager();
    }
}
