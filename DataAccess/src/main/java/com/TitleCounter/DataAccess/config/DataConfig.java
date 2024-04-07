package com.TitleCounter.DataAccess.config;

import com.TitleCounter.DataAccess.storage.entity.Game;
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

import java.time.LocalDate;

@Configuration
@RequiredArgsConstructor
public class DataConfig {
    private final RoleRepository roleRepository;
    private final UserRepository userRepository;
    private final GameRepository gameRepository;


    private final PasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

    @Bean
    public CommandLineRunner dataLoader() {
        Role admin = Role.builder()
                .id(1L)
                .name("ADMIN")
                .build();
        roleRepository.save(admin);
        Role user = Role.builder()
                .id(2L)
                .name("USER")
                .build();
        roleRepository.save(user);

        userRepository.save(User.builder()
                .username("admin")
                .password(passwordEncoder.encode("1111"))
                .email("zxc@gm.kukuasd")
                .roles(roleRepository.findAll())
                .build()
        );

        gameRepository.save(Game.builder()
                .id(1L)
                .time(0L)
                .title("a")
                .globalScore(0.0f)
                .dateRelease(LocalDate.now())
                .imageUrl("http://localhost")
                .linkUrl("http://localhost")
                .build());
        return (args) -> {
            for (String arg : args) {
                System.out.println(arg);
            }
        };
    }
}
