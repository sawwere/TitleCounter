package com.TitleCounter.DataAccess.config;

import com.TitleCounter.DataAccess.service.DefaultFileStorageService;
import com.TitleCounter.DataAccess.service.ImageStorageService;
import com.TitleCounter.DataAccess.service.StorageService;
import com.TitleCounter.DataAccess.storage.entity.Role;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.GameRepository;
import com.TitleCounter.DataAccess.storage.repository.RoleRepository;
import com.TitleCounter.DataAccess.storage.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Optional;

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
