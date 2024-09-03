package com.sawwere.titlecounter.backend.app.repository;

import com.sawwere.titlecounter.backend.app.BasicTestContainerTest;
import com.sawwere.titlecounter.backend.app.storage.entity.Role;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.RoleRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.UserRepository;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.jdbc.Sql;

import java.util.List;

@DataJpaTest
@ActiveProfiles("test")
@TestPropertySource("/application-test.yml")
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
@Sql(value = {"/sql/create_roles.sql"}, executionPhase = Sql.ExecutionPhase.BEFORE_TEST_CLASS)
public class UserRepositoryTest extends BasicTestContainerTest {
    @Autowired
    private UserRepository underTest;
    @Autowired
    private RoleRepository roleRepository;

    @AfterEach
    void cleanUp() {
        underTest.deleteAll();
    }

    @Test
    void findByUsername() {
        List<Role> roles = roleRepository.findAll();
        String username = "test";
        User expected = User.builder()
                .username(username)
                .password(new BCryptPasswordEncoder().encode("password"))
                .email("test@zxcvasdfvcxz.gre")
                .roles(roles)
                .build();
        underTest.save(expected);

        //when
        User actual = underTest.findByUsername(username).orElseThrow();

        //result
        Assertions.assertEquals(expected, actual);
    }
}
