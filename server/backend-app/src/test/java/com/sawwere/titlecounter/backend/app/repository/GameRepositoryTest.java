package com.sawwere.titlecounter.backend.app.repository;

import com.sawwere.titlecounter.backend.app.BasicTestContainerTest;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GameExternalId;
import com.sawwere.titlecounter.backend.app.storage.repository.GameRepository;
import org.joda.time.DateTime;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.jdbc.Sql;

import java.time.DateTimeException;
import java.time.LocalDate;
import java.time.LocalDateTime;

@DataJpaTest
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
@ActiveProfiles("test")
@TestPropertySource("/application-test.yml")
@Sql(value = {"/sql/create_games.sql"}, executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD)
public class GameRepositoryTest extends BasicTestContainerTest {
    @Autowired
    private GameRepository underTest;

    @AfterEach
    void cleanUp() {
        underTest.deleteAll();
    }

    @Test

    void findByHltbId() {
        Game expected = Game.builder()
                .id(1L)
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalId.builder()
                                .hltbId("5351")
                                .steamId(null)
                                .build()
                )
                .time(335)
                .title("LocoRoco")
                .createdAt(LocalDateTime.parse("2023-02-04T10:23:54.0"))
                .updatedAt(LocalDateTime.parse("2024-10-03T22:55:15.252577"))
                .gameType("game")
                .description(null)
                .build();


        Game actual = underTest.findByExternalId_HltbId("5351").orElseThrow();

        //result
        Assertions.assertEquals(expected.getId(), actual.getId());
        Assertions.assertEquals(expected.getTitle(), actual.getTitle());
        Assertions.assertEquals(expected.getGameType(), actual.getGameType());
        Assertions.assertEquals(expected.getCreatedAt(), actual.getCreatedAt());
    }

    @Test
    void findById() {
        Game expected = Game.builder()
                .id(1L)
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalId.builder()
                                .hltbId("5351")
                                .steamId(null)
                                .build()
                )
                .time(335)
                .title("LocoRoco")
                .createdAt(LocalDateTime.parse("2023-02-04T10:23:54.0"))
                .updatedAt(LocalDateTime.parse("2024-10-03T22:55:15.252577"))
                .gameType("game")
                .description(null)
                .build();

        //when
        Game actual = underTest.findById(1L).orElseThrow();

        //result
        Assertions.assertEquals(expected.getId(), actual.getId());
        Assertions.assertEquals(expected.getTitle(), actual.getTitle());
        Assertions.assertEquals(expected.getGameType(), actual.getGameType());
        Assertions.assertEquals(expected.getCreatedAt(), actual.getCreatedAt());
    }
}
