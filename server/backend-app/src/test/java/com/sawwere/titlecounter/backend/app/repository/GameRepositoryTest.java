package com.sawwere.titlecounter.backend.app.repository;

import com.sawwere.titlecounter.backend.app.BasicTestContainerTest;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.repository.GameRepository;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.TestPropertySource;

import java.time.LocalDate;

@DataJpaTest
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
@ActiveProfiles("test")
@TestPropertySource("/application-test.yml")
public class GameRepositoryTest extends BasicTestContainerTest {
    @Autowired
    private GameRepository underTest;

    @AfterEach
    void cleanUp() {
        underTest.deleteAll();
    }

    @Test
    void findById() {
        Game expected = Game.builder()
                .time(10L)
                .title("a")
                .dateRelease(LocalDate.EPOCH)
                .globalScore(5.5f)
                .linkUrl("http://localhost/images/games/1.jpg")
                .build();
        underTest.save(expected);

        //when
        Game actual = underTest.findById(1L).orElseThrow();

        //result
        Assertions.assertEquals(expected.getCreatedAt(), actual.getCreatedAt());
        Assertions.assertEquals(expected.getTitle(), actual.getTitle());
    }
}
