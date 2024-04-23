package com.TitleCounter.DataAccess.config;

import com.TitleCounter.DataAccess.controller.api.GameController;
import com.TitleCounter.DataAccess.service.UserService;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.ProviderManager;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.session.SessionInformationExpiredStrategy;

import java.util.Optional;

import static org.springframework.security.config.Customizer.withDefaults;


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
    public BCryptPasswordEncoder bCryptPasswordEncoder() {
        return new BCryptPasswordEncoder();
    }
    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http
                .csrf(csrf -> csrf.disable())
                .authorizeHttpRequests((auth) -> auth
                        .requestMatchers("/api/login").anonymous()
                        .requestMatchers("/", "/error", "/login",
                                "/registration",
                                "/games/*", "/users/**",
                                "/css/**", "/images/**").permitAll()
                        .requestMatchers("/games/*/submit").authenticated()
                        //.requestMatchers(HttpMethod.POST, GameController.CREATE_GAME).hasAuthority("SCOPE_addTitles")
                        .requestMatchers(
                                HttpMethod.DELETE,
                                GameController.DELETE_GAME
                        ).hasAuthority("SCOPE_deleteTitles")
                        .requestMatchers(
                                HttpMethod.POST,
                                GameController.CREATE_GAME_ENTRY
                        ).authenticated()
                        .requestMatchers(
                                HttpMethod.PUT,
                                GameController.UPDATE_GAME_ENTRY
                        ).authenticated()
                        .requestMatchers(
                                HttpMethod.DELETE,
                                GameController.DELETE_GAME_ENTRY
                        ).authenticated()
                        .anyRequest().authenticated()
                )
                .sessionManagement(session -> session
                        .invalidSessionUrl("/login")
                        .maximumSessions(1))
                //.httpBasic(withDefaults())
                .formLogin(form -> form
                        .defaultSuccessUrl("/")
                        .permitAll())
                .logout(logout -> logout
                        .invalidateHttpSession(true)
                        .deleteCookies("JSESSIONID")
                        .logoutSuccessUrl("/"))
                .rememberMe(x->x
                       .rememberMeParameter("remember-me-new")
                        .alwaysRemember(false)
                        .userDetailsService(userService)
                        .tokenValiditySeconds(60*60*24))
                //.oauth2ResourceServer(oath2 -> oath2.jwt(withDefaults()))
                ;
        return http.build();
    }

//    @Bean
//    public AuthenticationManager authenticationManager(
//            UserDetailsService userDetailsService,
//            PasswordEncoder passwordEncoder) {
//        DaoAuthenticationProvider authenticationProvider = new DaoAuthenticationProvider();
//        authenticationProvider.setUserDetailsService(userDetailsService);
//        authenticationProvider.setPasswordEncoder(passwordEncoder);
//
//        return new ProviderManager(authenticationProvider);
//    }

    @Autowired
    void configure(AuthenticationManagerBuilder builder) throws Exception {
        builder.userDetailsService(userService).passwordEncoder(new BCryptPasswordEncoder());
    }
}
