package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.user.UserDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Role;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.RoleRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.UserRepository;
import com.sawwere.titlecounter.common.dto.notification.NotificationDto;
import com.sawwere.titlecounter.common.dto.role.RoleDto;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class UserService implements UserDetailsService {
    private final UserRepository userRepository;
    private final RoleRepository roleRepository;
    private final RabbitProducerService rabbitProducerService;

    private final UserDtoFactory userDtoFactory;

    public User findUserOrElseThrowException(Long userId) {
        return userRepository.findById(userId)
                .orElseThrow(() -> new NotFoundException(String.format("User with id '%s' doesn't exist", userId))
                );
    }

    public List<User> allUsers() {
        return userRepository.findAll();
    }

    @Transactional
    public User createUser(UserRegistrationDto userRegistrationDto) {
        Optional<User> optionalUser = userRepository.findByUsername(userRegistrationDto.getUsername());
        if (optionalUser.isPresent())
            throw new RuntimeException("User already exists");
//        optionalUser = userRepository.findByEmail(userRegistrationDto.getEmail());
//        if (optionalUser.isPresent())
//            throw new RuntimeException("User already exists");

        User user = User.builder()
                .email(userRegistrationDto.getEmail())
                .password(userRegistrationDto.getPassword())
                .username(userRegistrationDto.getUsername())
                .build();
        user.setRoles(List.of(roleRepository.findByName("USER").get()));
        user.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
        userRepository.save(user);
        rabbitProducerService.send(userDtoFactory.entityToDto(user));
        return user;
    }

    public void deleteUser(Long userId) {
        User user = findUserOrElseThrowException(userId);
        userRepository.deleteById(userId);
    }

    public User findUserByUsername(String username) throws UsernameNotFoundException {
        Optional<User> optionalUser = userRepository.findByUsername(username);

        if (optionalUser.isEmpty()) {
            throw new UsernameNotFoundException("User not found");
        }

        return optionalUser.get();
    }

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        return findUserByUsername(username);
    }

    @Transactional
    public void addRole(Long userId, RoleDto roleDto) {
        User user = findUserOrElseThrowException(userId);
        Role role = roleRepository.findByName(roleDto.getName())
                .orElseThrow(() -> new NotFoundException("Invalid role name given"));
        user.getRoles().add(role);
        userRepository.save(user);
    }

    public User getCurrentUser() {
        // Получение имени пользователя из контекста Spring Security
        var username = SecurityContextHolder.getContext().getAuthentication().getName();
        return findUserByUsername(username);
    }
}
