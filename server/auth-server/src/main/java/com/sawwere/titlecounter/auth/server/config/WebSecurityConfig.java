package com.sawwere.titlecounter.auth.server.config;

import com.sawwere.titlecounter.auth.server.storage.entity.User;
import com.sawwere.titlecounter.auth.server.storage.repository.UserRepository;
import java.util.Optional;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.Customizer;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class WebSecurityConfig {
    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

    @Bean
    public UserDetailsService userDetailsService(UserRepository userRepo) {
        return username -> {
            Optional<User> user = userRepo.findByUsername(username);
            if (user.isPresent()) {
                return user.get();
            } else {
                throw new UsernameNotFoundException("User ‘" + username + "’ not found");
            }
        };
    }

    @SuppressWarnings({"checkstyle:NoWhitespaceBefore", "checkstyle:LineLength"})
    @Bean
    public SecurityFilterChain defaultSecurityChain(HttpSecurity http) throws Exception {
        http
                .authorizeHttpRequests((auth) -> auth
                        .requestMatchers("/error", "/api/login").permitAll()
                        .anyRequest().authenticated()
                )
                .formLogin(Customizer.withDefaults())
//                .oauth2Login(httpSecurityOAuth2LoginConfigurer -> httpSecurityOAuth2LoginConfigurer
//                        .loginPage("/abc")
//                        .tokenEndpoint(new Customizer<OAuth2LoginConfigurer<org.springframework.security.config.annotation.web.builders.HttpSecurity>.TokenEndpointConfig>() {
//                            @Override
//                            public void customize(OAuth2LoginConfigurer<HttpSecurity>.TokenEndpointConfig tokenEndpointConfig) {
//                                tokenEndpointConfig.
//                            }
//                        })
//                )
        ;
        return http.build();
    }
}
