package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.exception.AlreadyExistsException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Role;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.RoleRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.UserRepository;
import com.sawwere.titlecounter.common.dto.role.RoleDto;
import java.util.List;
import java.util.Optional;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@RequiredArgsConstructor
public class UserService implements UserDetailsService {
    private final UserRepository userRepository;
    private final RoleRepository roleRepository;

    /**
     * Get user by their id
     * @param userId the id of the user
     * @return user with specified id
     * @throws NotFoundException in case there is no user with such id
     */
    public User findUserOrElseThrowException(Long userId) throws NotFoundException {
        return userRepository.findById(userId)
                .orElseThrow(() -> new NotFoundException(String.format("User with id '%s' doesn't exist", userId))
                );
    }

    /**
     * Returns all instances of users
     * @return new list
     */
    public List<User> allUsers() {
        return userRepository.findAll();
    }

    /**
     * Creates user with given credentials
     * @param userRegistrationDto the dto object storing user info
     * @return entity of created user
     * @throws AlreadyExistsException in case there already is user with given credentials
     */
    @Transactional
    public User createUser(UserRegistrationDto userRegistrationDto) throws AlreadyExistsException {
        if (userRepository.existsByUsernameOrEmail(userRegistrationDto.getUsername(), userRegistrationDto.getEmail())) {
            throw new AlreadyExistsException("User already exists");
        }

        User user = User.builder()
                .email(userRegistrationDto.getEmail())
                .password(userRegistrationDto.getPassword())
                .username(userRegistrationDto.getUsername())
                .build();
        user.setRoles(List.of(roleRepository.findByName("USER").orElseThrow()));
        user.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
        userRepository.save(user);
        return user;
    }

    /**
     * Deletes user by their id
     * @param userId id of user
     * @throws NotFoundException in case there is no user with such id
     */
    public void deleteUser(Long userId) throws NotFoundException {
        User user = findUserOrElseThrowException(userId);
        userRepository.deleteById(userId);
    }

    /**
     * Get user by their username
     * @param username the username of the user
     * @return ser with specified username
     * @throws NotFoundException in case there is no user with such id
     */
    public User findUserByUsername(String username) throws NotFoundException {
        Optional<User> optionalUser = userRepository.findByUsername(username);

        if (optionalUser.isEmpty()) {
            throw new NotFoundException("User not found");
        }

        return optionalUser.get();
    }

    /**
     * Get UserDetails of user by their username
     * @param username the username identifying the user whose data is required
     * @return user with such username
     * @throws NotFoundException in case there is no user with such username
     */
    @Override
    public UserDetails loadUserByUsername(String username) throws NotFoundException {
        return findUserByUsername(username);
    }

    /**
     * Enables user by their id
     * @param userId the id of the user who needs to be enabled
     */
    public void enableUser(Long userId) {
        User user = findUserOrElseThrowException(userId);
        user.setIsEnabled(true);
        userRepository.save(user);
    }

    /**
     * Adds role to the user
     * @param userId the id of the user whose roles needs to be modified
     * @param roleDto role to be added
     */
    @Transactional
    public void addRole(Long userId, RoleDto roleDto) {
        User user = findUserOrElseThrowException(userId);
        Role role = roleRepository.findByName(roleDto.getName())
                .orElseThrow(() -> new NotFoundException("Invalid role name given"));
        user.getRoles().add(role);
        userRepository.save(user);
    }

    /**
     * Get current user based on security context
     * @return Authenticated user object
     */
    public User getCurrentUser() {
        var username = SecurityContextHolder.getContext().getAuthentication().getName();
        return findUserByUsername(username);
    }
}
