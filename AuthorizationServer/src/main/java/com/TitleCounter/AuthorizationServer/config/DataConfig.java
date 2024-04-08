package com.TitleCounter.AuthorizationServer.config;

import com.TitleCounter.AuthorizationServer.storage.entity.Role;
import com.TitleCounter.AuthorizationServer.storage.entity.User;
import com.TitleCounter.AuthorizationServer.storage.repository.RoleRepository;
import com.TitleCounter.AuthorizationServer.storage.repository.UserRepository;
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
    private final RoleRepository roleRepository;
    private final UserRepository userRepository;

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

        Optional<User> userEntity = userRepository.findByUsername("admin");
        if (userEntity.isEmpty())
            userRepository.save(User.builder()

                    .username("admin")
                    .password(passwordEncoder.encode("1111"))
                    .email("zxc@gm.kukuasd")
                    .roles(roleRepository.findAll())
                    .build()
            );
        return (args) -> {
            for (String arg : args) {
                System.out.println(arg);
            }
        };
    }
}
