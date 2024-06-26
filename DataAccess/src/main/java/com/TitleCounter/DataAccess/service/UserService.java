package com.TitleCounter.DataAccess.service;

import com.TitleCounter.DataAccess.dto.user.UserRegistrationDto;
import com.TitleCounter.DataAccess.exception.NotFoundException;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.RoleRepository;
import com.TitleCounter.DataAccess.storage.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class UserService implements UserDetailsService {
    private  final UserRepository userRepository;
    private  final RoleRepository roleRepository;

    public User findUserById(Long userId) {
        Optional<User> userFromDb = userRepository.findById(userId);
        return userFromDb.orElse(new User());
    }

    public User findUserOrElseThrowException(Long userId) {
        return userRepository.findById(userId)
                .orElseThrow(() -> new NotFoundException(String.format("User with id '%s' doesn't exist", userId))
                );
    }

    public List<User> allUsers() {
        return userRepository.findAll();
    }

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
        System.out.println(user.getUsername());
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
}
