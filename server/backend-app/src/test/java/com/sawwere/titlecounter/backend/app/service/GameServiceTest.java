package com.sawwere.titlecounter.backend.app.service;


import com.sawwere.titlecounter.backend.app.BasicTestContainerTest;
import com.sawwere.titlecounter.backend.app.TitleCounterBackendApplication;
import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GameDeveloper;
import com.sawwere.titlecounter.backend.app.storage.entity.GameEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.GameExternalId;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.GameEntryRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.UserRepository;
import com.sawwere.titlecounter.common.dto.game.GameDeveloperDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryRequestDto;
import com.sawwere.titlecounter.common.dto.game.GameExternalIdDto;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.jdbc.AutoConfigureTestDatabase;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.jdbc.Sql;

@SpringBootTest(classes = TitleCounterBackendApplication.class)
@AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
@ActiveProfiles("test")
@TestPropertySource("/application-test.yml")
@Sql(value = {"/sql/create_user.sql"}, executionPhase = Sql.ExecutionPhase.BEFORE_TEST_CLASS)
@Sql(value = {"/sql/create_games.sql"}, executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD)
class GameServiceTest extends BasicTestContainerTest {
    @Autowired
    private GameService gameService;

    @Autowired
    private GameRepository gameRepository;
    @Autowired
    private GameEntryRepository gameEntryRepository;
    @Autowired
    private UserRepository userRepository;

    @AfterEach
    void cleanUp() {
        gameRepository.deleteAll();
        gameEntryRepository.deleteAll();
    }

    @Test
    void findGame() {
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
                .platforms(new ArrayList<>())
                .developer(null)
                .developers(new ArrayList<>())
                .genres(new ArrayList<>())
                .time(335)
                .title("LocoRoco")
                .createdAt(LocalDateTime.parse("2023-02-04T10:23:54.0"))
                .updatedAt(LocalDateTime.parse("2024-10-03T22:55:15.252577"))
                .gameType("game")
                .description(null)
                .build();

        Optional<Game> actual = gameService.findGame(1L);

        //result
        Assertions.assertTrue(actual.isPresent());
        Assertions.assertEquals(expected.getId(), actual.get().getId());
        Assertions.assertEquals(expected.getTitle(), actual.get().getTitle());
        Assertions.assertEquals(expected.getGameType(), actual.get().getGameType());
        Assertions.assertEquals(expected.getCreatedAt(), actual.get().getCreatedAt());
    }

    @Test
    void findGameOrElseThrowException() {
        //result
        Assertions.assertThrows(NotFoundException.class, () -> gameService.findGameOrElseThrowException(-1L));
    }

    @Test
    void findGameEntryOrElseThrowException() {
        Assertions.assertThrows(NotFoundException.class, () -> gameService.findGameEntryOrElseThrowException(-1L));
    }

    @Test
    void createGameDeveloper() {
        GameDeveloperDto dto = GameDeveloperDto.builder()
                .name("_GameDeveloper")
                .build();
        GameDeveloper gameDeveloper = gameService.createGameDeveloper(dto);

        Assertions.assertEquals(dto.getName(), gameDeveloper.getName());
        Assertions.assertTrue(gameDeveloper.getId() > 0);
    }

    @Test
    void createGame() {
        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game")
                .platforms(new ArrayList<>())
                .genres(new ArrayList<>())
                .developer("")
                .gameType("game")
                .imageUrl(null)
                .description(null)
                .build();

        Game actual = gameService.createGame(dto);

        Assertions.assertTrue(actual.getId() > 0);
    }

    @Test
    void createGameWithPlatforms() {
        List<String> platforms = List.of("PC", "Xbox 360");

        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game")
                .platforms(platforms)
                .genres(new ArrayList<>())
                .developer(null)
                .gameType("game")
                .imageUrl(null)
                .description(null)
                .build();

        Game actual = gameService.createGame(dto);

        Assertions.assertTrue(actual.getId() > 0);
        Assertions.assertEquals(actual.getPlatforms().size(), platforms.size());
    }

    @Test
    void createGameWithGenres() {
        List<String> genres = List.of("Action", "Sports", "new genre");

        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game")
                .platforms(new ArrayList<>())
                .genres(genres)
                .developer(null)
                .gameType("game")
                .imageUrl(null)
                .description(null)
                .build();

        Game actual = gameService.createGame(dto);

        Assertions.assertTrue(actual.getId() > 0);
        Assertions.assertEquals(actual.getGenres().size(), genres.size());
    }

    @Test
    void createGameWithDevelopers() {
        List<String> developers = List.of("A", "B", "C");

        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game")
                .platforms(new ArrayList<>())
                .genres(new ArrayList<>())
                .developer(String.join(",", developers))
                .gameType("game")
                .imageUrl(null)
                .description(null)
                .build();

        Game actual = gameService.createGame(dto);

        Assertions.assertTrue(actual.getId() > 0);
        Assertions.assertEquals(developers.size(), actual.getDevelopers().size());
        Assertions.assertArrayEquals(
                developers.toArray(),
                actual.getDevelopers()
                        .stream()
                        .map(GameDeveloper::getName)
                        .toArray()
        );
    }

    @Test
    void updateGame() {
        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game_new")
                .platforms(List.of("PC", "PlayStation 5"))
                .genres(new ArrayList<>())
                .developer(null)
                .gameType("dlc")
                .description("new_descriptions")
                .build();
        Long gameId = 1L;

        Game actual = gameService.updateGame(gameId, dto);

        Assertions.assertEquals(gameId, actual.getId());
        Assertions.assertEquals(dto.getTitle(), actual.getTitle());
        Assertions.assertEquals(dto.getGameType(), actual.getGameType());
        Assertions.assertEquals(dto.getDescription(), actual.getDescription());
        Assertions.assertEquals(dto.getPlatforms().size(), actual.getPlatforms().size());
    }

    @Test
    void updateGameWithGenres() {
        List<String> genres = List.of("Action", "Sports");

        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game_new")
                .platforms(new ArrayList<>())
                .genres(genres)
                .developer(null)
                .gameType("dlc")
                .description("new_descriptions")
                .build();
        Long gameId = 1L;

        Game actual = gameService.updateGame(gameId, dto);

        Assertions.assertEquals(gameId, actual.getId());
        Assertions.assertEquals(dto.getTitle(), actual.getTitle());
        Assertions.assertEquals(dto.getGameType(), actual.getGameType());
        Assertions.assertEquals(dto.getDescription(), actual.getDescription());
        Assertions.assertEquals(genres.size(), actual.getGenres().size());
    }

    @Test
    void updateGameWithDevelopers() {
        List<String> developers = List.of("A", "B", "C");

        GameCreationDto dto = GameCreationDto.builder()
                .dateRelease(LocalDate.of(2006,6,23))
                .globalScore(7.2f)
                .externalId(
                        GameExternalIdDto.builder()
                                .hltbId("_12345678_")
                                .steamId(null)
                                .build()
                )
                .time(335L)
                .title("__test_game_new")
                .platforms(new ArrayList<>())
                .genres(new ArrayList<>())
                .developer(String.join(",", developers))
                .gameType("dlc")
                .description("new_descriptions")
                .build();
        Long gameId = 1L;

        Game actual = gameService.updateGame(gameId, dto);

        Assertions.assertEquals(gameId, actual.getId());
        Assertions.assertEquals(dto.getTitle(), actual.getTitle());
        Assertions.assertEquals(dto.getGameType(), actual.getGameType());
        Assertions.assertEquals(dto.getDescription(), actual.getDescription());
        Assertions.assertArrayEquals(
                developers.toArray(),
                actual.getDevelopers()
                        .stream()
                        .map(GameDeveloper::getName)
                        .toArray()
        );
    }

    @Test
    void deleteGame() {
        var countBefore = gameRepository.count();

        gameService.deleteGame(1L);

        var actual = gameRepository.count();

        Assertions.assertEquals(countBefore - 1, actual);
    }

    @Test
    void search() {
        var actual = gameService.search(
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                0,
                10
        );

        Assertions.assertEquals(actual.getSize(), 10);
    }

    @Test
    void searchWithAllParams() {
        var actual = gameService.search(
                "",
                0L,
                99999999999L,
                0f,
                10f,
                LocalDate.EPOCH,
                LocalDate.of(9999, 12, 31),
                null,
                0,
                10
        );

        Assertions.assertEquals(actual.getSize(), 10);
    }

    @Test
    void createGameEntry() {
        User user = userRepository.findAll().getFirst();
        Game game = gameRepository.findById(1L).orElseThrow();
        GameEntryRequestDto expected = GameEntryRequestDto.builder()
                .gameId(game.getId())
                .platform("PC")
                .time(5L)
                .dateCompleted(LocalDate.EPOCH)
                .status("completed")
                .userId(user.getId())
                .build();

        GameEntry actual = gameService.createGameEntry(user.getUsername(), expected);

        Assertions.assertEquals(game.getTitle(), actual.getCustomTitle());
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void updateGameEntry() {
        User user = userRepository.findAll().getFirst();
        var gameEntries = gameService.findGameEntriesByUser(user.getUsername());

        GameEntryRequestDto expected = GameEntryRequestDto.builder()
                .id(gameEntries.getFirst().getId())
                .gameId(1L)
                .userId(user.getId())
                .platform("PC")
                .time(5L)
                .customTitle("none")
                .dateCompleted(LocalDate.EPOCH)
                .status("completed")
                .build();

        GameEntry actual = gameService.updateGameEntry(
                gameEntries.getFirst().getId(),
                expected,
                user.getUsername()
        );

        Assertions.assertEquals(expected.getCustomTitle(), actual.getCustomTitle());
        Assertions.assertEquals(expected.getTime(), actual.getTime());
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void updateGameEntryInvalidUser() {
        User userActual = userRepository.findAll().get(0);
        User userInvalid = userRepository.findAll().get(1);
        var gameEntries = gameService.findGameEntriesByUser(userActual.getUsername());

        GameEntryRequestDto expected = GameEntryRequestDto.builder()
                .id(gameEntries.getFirst().getId())
                .gameId(1L)
                .userId(userActual.getId())
                .platform("PC")
                .time(5L)
                .customTitle("none")
                .dateCompleted(LocalDate.EPOCH)
                .status("completed")
                .build();

        Assertions.assertThrows(ForbiddenException.class,
                () -> gameService.updateGameEntry(
                        gameEntries.getFirst().getId(),
                        expected,
                        userInvalid.getUsername())
        );
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void updateGameEntryInvalidId() {
        User user = userRepository.findAll().getFirst();
        var gameEntries = gameService.findGameEntriesByUser(user.getUsername());

        GameEntryRequestDto expected = GameEntryRequestDto.builder()
                .id(1004L)
                .gameId(1L)
                .userId(user.getId())
                .platform("PC")
                .time(5L)
                .customTitle("none")
                .dateCompleted(LocalDate.EPOCH)
                .status("completed")
                .build();

        Assertions.assertThrows(IllegalArgumentException.class,
                () -> gameService.updateGameEntry(
                        gameEntries.getFirst().getId(),
                        expected,
                        user.getUsername())
        );
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void deleteGameEntry() {
        User user = userRepository.findAll().getFirst();
        var gameEntries = gameService.findGameEntriesByUser(user.getUsername());


        gameService.deleteGameEntry(
                gameEntries.getFirst().getId(),
                user.getUsername()
        );

        Assertions.assertEquals(
                gameEntries.size() - 1,
                gameService.findGameEntriesByUser(user.getUsername()).size()
        );
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void deleteGameEntryInvalidUser() {
        User userActual = userRepository.findAll().get(0);
        User userInvalid = userRepository.findAll().get(1);
        var gameEntries = gameService.findGameEntriesByUser(userActual.getUsername());

        Assertions.assertThrows(
                ForbiddenException.class,
                () -> gameService.deleteGameEntry(
                        gameEntries.getFirst().getId(),
                        userInvalid.getUsername()
                )
        );
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void findGameEntriesByUserId() {
        User user = userRepository.findAll().getFirst();

        List<GameEntry> actual = gameService.findGameEntriesByUser(user.getId());

        Assertions.assertEquals(actual.size(), 5);
    }

    @Test
    @Sql(
            value = {"/sql/create_games.sql", "/sql/create_game_entries.sql"},
            executionPhase = Sql.ExecutionPhase.BEFORE_TEST_METHOD
    )
    void findGameEntriesByUserUsername() {
        User user = userRepository.findAll().getFirst();

        List<GameEntry> actual = gameService.findGameEntriesByUser(user.getUsername());

        Assertions.assertEquals(actual.size(), 5);
    }

    @Test
    void getStatistics() {
    }
}