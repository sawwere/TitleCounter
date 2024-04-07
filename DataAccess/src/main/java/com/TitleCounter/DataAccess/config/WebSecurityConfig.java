package com.TitleCounter.DataAccess.config;

import com.TitleCounter.DataAccess.service.UserService;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;

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
                        .requestMatchers("/news").authenticated()
                        .anyRequest().permitAll()
                )
                .httpBasic(withDefaults())
                .formLogin(form -> form
                        .permitAll()
                        .defaultSuccessUrl("/"))
                .logout(logout -> logout
                        .invalidateHttpSession(true)
                        .deleteCookies("JSESSIONID")
                        .logoutSuccessUrl("/login"))
                ;
        return http.build();
    }

    @Autowired
    void configure(AuthenticationManagerBuilder builder) throws Exception {
        builder.userDetailsService(userService).passwordEncoder(new BCryptPasswordEncoder());
    }
}
