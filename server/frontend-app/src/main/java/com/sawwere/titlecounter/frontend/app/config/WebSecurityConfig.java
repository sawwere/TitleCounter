package com.sawwere.titlecounter.frontend.app.config;

import com.sawwere.titlecounter.frontend.app.controller.GameController;
import com.sawwere.titlecounter.frontend.app.controller.UserController;
import com.sawwere.titlecounter.frontend.app.service.ApiClient;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.method.configuration.EnableMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;


@Configuration
@EnableWebSecurity
@EnableMethodSecurity
@RequiredArgsConstructor
public class WebSecurityConfig {
    private final AuthenticationFilter authenticationFilter;
    private final ApiClient apiClient;

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http, AuthenticationManager authenticationManager) throws Exception {
        http
                .csrf(AbstractHttpConfigurer::disable)
                .authorizeHttpRequests((auth) -> auth
                        .requestMatchers(UserController.LOGIN, UserController.REGISTRATION).anonymous()
                        .requestMatchers(UserController.LOGOUT).authenticated()
                        .requestMatchers(
                                HttpMethod.GET, GameController.GET_ALL_GAMES, GameController.GET_GAME,
                                GameController.GET_USER_GAME_ENTRIES,
                                UserController.GET_USER).permitAll()
                        .requestMatchers("/", "/error","/users",
                                "/css/**", "/images/**").permitAll()
                        .requestMatchers("/games/*/submit").authenticated()
                        .anyRequest().permitAll()
                )
                .sessionManagement(manager -> manager.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
                .addFilterBefore(authenticationFilter, UsernamePasswordAuthenticationFilter.class)
                .formLogin(form -> form
                        .loginPage(UserController.LOGIN)
                        .defaultSuccessUrl("/")
                        .permitAll())
//                .logout(logout -> logout
//                        .invalidateHttpSession(true)
//                        .deleteCookies("JSESSIONID", "SESSION")
//                        .logoutSuccessHandler(new HttpStatusReturningLogoutSuccessHandler()))
                //.exceptionHandling(ex -> ex.authenticationEntryPoint(new HttpStatusEntryPoint(HttpStatus.UNAUTHORIZED)))
                ;
        return http.build();
    }

    @Bean
    public BCryptPasswordEncoder bCryptPasswordEncoder() {
        return new BCryptPasswordEncoder();
    }

    @Bean
    public AuthenticationManager authenticationManager(AuthenticationConfiguration config)
            throws Exception {
        return config.getAuthenticationManager();
    }
}
