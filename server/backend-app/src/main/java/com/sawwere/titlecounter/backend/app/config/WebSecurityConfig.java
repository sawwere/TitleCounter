package com.sawwere.titlecounter.backend.app.config;


import com.sawwere.titlecounter.backend.app.controller.api.AuthController;
import com.sawwere.titlecounter.backend.app.controller.api.FilmController;
import com.sawwere.titlecounter.backend.app.controller.api.GameController;
import com.sawwere.titlecounter.backend.app.controller.api.UserController;
import com.sawwere.titlecounter.backend.app.controller.view.ImageController;
import com.sawwere.titlecounter.backend.app.service.JwtService;
import com.sawwere.titlecounter.backend.app.service.UserService;
import java.util.List;
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
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.HttpStatusEntryPoint;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;
import org.springframework.security.web.util.matcher.RequestMatcher;
import org.springframework.web.cors.CorsConfiguration;
import static com.sawwere.titlecounter.backend.app.controller.api.AuthController.REFRESH_TOKEN;


@Configuration
@EnableWebSecurity
@EnableMethodSecurity
@RequiredArgsConstructor
public class WebSecurityConfig {
    private final AuthenticationFilter authenticationFilter;
    private final UserService userService;
    private final JwtService jwtService;

    private static final String ADMIN_ROLE_NAME = "ADMIN";

    @Bean
    public UserDetailsService userDetailsService() {
        return userService;
    }

    @Bean
    public RequestMatcher apiHttpGetRequestMatcher() {
        return request -> {
            String requestUri = request.getRequestURI();
            return request.getMethod().equals(HttpMethod.GET.toString())
                    && (requestUri.equals(GameController.FIND_GAME)
                    || requestUri.equals(GameController.SEARCH_GAMES)
                    || requestUri.equals(GameController.FIND_GAME_ENTRIES)
                    || requestUri.equals(FilmController.FIND_FILM)
                    || requestUri.equals(FilmController.FIND_ALL_FILMS)
                    || requestUri.equals(FilmController.FIND_FILM_ENTRIES));
        };
    }

    @Bean
    public SecurityFilterChain filterChain(
            HttpSecurity http,
            AuthenticationManager authenticationManager) throws Exception {
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
                        .requestMatchers(AuthController.API_LOGIN, AuthController.API_REGISTER).anonymous()
                        .requestMatchers(AuthController.API_LOGOUT).authenticated()
                        .requestMatchers(
                                HttpMethod.GET, ImageController.GET,
                                "/v3/api-docs/**",
                                "/api-docs/**",
                                "/swagger-resources/**",
                                "/swagger-ui/**",
                                GameController.FIND_GAME, UserController.FIND_USER,
                                GameController.SEARCH_GAMES, GameController.FIND_GAME_ENTRIES,
                                FilmController.FIND_FILM, FilmController.FIND_ALL_FILMS,
                                FilmController.FIND_FILM_ENTRIES
                        ).permitAll()
                        .requestMatchers("/", "/error", "/users", REFRESH_TOKEN,
                                "/login", "/registration",
                                "/games/*", "/games", "/users/**",
                                "/css/**", "/images/**").permitAll()
                        .requestMatchers("/games/*/submit").authenticated()
                        .requestMatchers(UserController.ADD_ROLE).hasAuthority(ADMIN_ROLE_NAME)
                        .requestMatchers(
                                HttpMethod.POST,
                                GameController.CREATE_GAME, FilmController.CREATE_FILM
                        ).hasAuthority(ADMIN_ROLE_NAME)
                        .requestMatchers(
                                HttpMethod.PUT,
                                GameController.UPDATE_GAME, FilmController.UPDATE_FILM
                        ).hasAuthority(ADMIN_ROLE_NAME)
                        .requestMatchers(
                                HttpMethod.DELETE,
                                GameController.DELETE_GAME, FilmController.DELETE_FILM
                        ).hasAuthority(ADMIN_ROLE_NAME)
                        .anyRequest().permitAll()
                )
                .sessionManagement(manager -> manager.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
                .authenticationProvider(authenticationProvider())
                .addFilter(customAuthenticationFilter(authenticationManager))
                .addFilterBefore(authenticationFilter, UsernamePasswordAuthenticationFilter.class)
                .exceptionHandling(ex -> ex.authenticationEntryPoint(new HttpStatusEntryPoint(HttpStatus.UNAUTHORIZED)));
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
