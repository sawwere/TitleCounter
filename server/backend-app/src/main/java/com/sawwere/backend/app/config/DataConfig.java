package com.sawwere.backend.app.config;

import com.sawwere.backend.app.service.DefaultFileStorageService;
import com.sawwere.backend.app.service.ImageStorageService;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

@Configuration
@RequiredArgsConstructor
public class DataConfig {
    private final PasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

    @Bean
    public CommandLineRunner dataLoader(DefaultFileStorageService storageService,
                                        ImageStorageService imageStorageService) {
        storageService.init();
        imageStorageService.init();

        return (args) -> {
            for (String arg : args) {
                System.out.println(arg);
            }
        };
    }
}
