package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.BasicTestContainerTest;
import com.sawwere.titlecounter.backend.app.TitleCounterBackendApplication;
import com.sawwere.titlecounter.backend.app.dto.user.UserRegistrationDto;
import com.sawwere.titlecounter.backend.app.exception.AlreadyExistsException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Role;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.RoleRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.UserRepository;
import com.sawwere.titlecounter.common.dto.role.RoleDto;
import java.util.HashSet;
import java.util.List;
import java.util.stream.Collectors;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.jdbc.Sql;
import org.springframework.transaction.annotation.Transactional;

@SpringBootTest(classes = TitleCounterBackendApplication.class)
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
@ActiveProfiles("test")
@TestPropertySource("/application-test.yml")
@Sql(value = {"/sql/create_user.sql"}, executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD)
@Sql(value = {"/sql/delete_user.sql"}, executionPhase = Sql.ExecutionPhase.AFTER_TEST_METHOD)
class UserServiceTest extends BasicTestContainerTest {
    @Autowired
    private UserService userService;

    @Autowired
    private UserRepository userRepository;
    @Autowired
    private RoleRepository roleRepository;

    @AfterEach
    void cleanUp() {

    }

    protected User getAdminUser() {
        return userRepository.findByUsername("admin").orElseThrow();
    }

    @Test
    void findUserOrElseThrow() {
        User expected = getAdminUser();
        long userId = expected.getId();

        User actual = userService.findUserOrElseThrowException(userId);

        Assertions.assertEquals(expected.getUsername(), actual.getUsername());
        Assertions.assertEquals(expected.getId(), actual.getId());
        Assertions.assertEquals(expected.getEmail(), actual.getEmail());
    }

    @Test
    void findUserOrElseThrowWithException() {
        long userId = 100500L;

        Assertions.assertThrows(
                NotFoundException.class,
                () -> userService.findUserOrElseThrowException(userId)
        );
    }

    @Test
    void allUsers() {
        var actual = userService.allUsers();

        Assertions.assertEquals(3, actual.size());
    }

    @Test
    void createUser() {
        UserRegistrationDto expected = UserRegistrationDto.builder()
                .username("new_user")
                .password("$2a85password")
                .passwordConfirm("$2a85password")
                .email("test@test.test")
                .build();

        User actual = userService.createUser(expected);

        Assertions.assertEquals(expected.getUsername(), actual.getUsername());
        Assertions.assertEquals(expected.getEmail(), actual.getEmail());
        Assertions.assertTrue(actual.getId() > 0);

        userRepository.delete(actual);
    }

    @Test
    void createUserNonUniqueName() {
        UserRegistrationDto expected = UserRegistrationDto.builder()
                .username(getAdminUser().getUsername())
                .password("$2a85password")
                .passwordConfirm("$2a85password")
                .email("test@test.test")
                .build();

        Assertions.assertThrows(
                AlreadyExistsException.class,
                () -> userService.createUser(expected)
        );
    }

    @Test
    @Transactional
    void deleteUser() {
        long expected = userRepository.count() - 1;
        User adminUser = getAdminUser();

        userService.deleteUser(adminUser.getId());

        Assertions.assertEquals(expected, userRepository.count());

        userRepository.save(adminUser);
    }

    @Test
    @Transactional
    void deleteUserInvalidId() {

        Assertions.assertThrows(
                NotFoundException.class,
                () -> userService.deleteUser(100500L)
        );
    }

    @Test
    void findUserByUsername() {
        String username = "admin";
        User expected = getAdminUser();

        User actual = userService.findUserByUsername(username);

        Assertions.assertEquals(username, actual.getUsername());
        Assertions.assertEquals(expected.getId(), actual.getId());
        Assertions.assertEquals(expected.getEmail(), actual.getEmail());
    }

    @Test
    void loadUserByUsername() {
        String username = "admin";
        User expected = getAdminUser();

        UserDetails actual = userService.loadUserByUsername(username);

        Assertions.assertEquals(username, actual.getUsername());
        Assertions.assertEquals(expected.getPassword(), actual.getPassword());
        Assertions.assertEquals(expected.getAuthorities().size(), actual.getAuthorities().size());
    }

    @Test
    void loadUserByUsernameWithException() {
        String username = "not_existing_username";

        Assertions.assertThrows(
                NotFoundException.class,
                () -> userService.loadUserByUsername(username)
        );
    }

    @Test
    void addRole() {
        User user = userRepository.findById(101L).orElseThrow();
        userService.addRole(user.getId(), RoleDto.builder().name("ADMIN").build());

        user = userRepository.findById(101L).orElseThrow();

        HashSet<String> expected = new HashSet<>(List.of("ADMIN", "USER"));

        Assertions.assertFalse(
                expected.retainAll( user.getRoles()
                                .stream()
                                .map(Role::getName)
                                .collect(Collectors.toSet()))
        );
    }

    @Test
    void addRoleInvalidName() {
        User user = userRepository.findById(101L).orElseThrow();

        Assertions.assertThrows(
                NotFoundException.class,
                () -> userService.addRole(
                        user.getId(), RoleDto.builder().name("ADMINa").build()
                )
        );
    }
}