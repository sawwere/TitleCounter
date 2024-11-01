package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.dto.game.GameEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.mapper.GameMapper;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.exception.StorageException;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GameDeveloper;
import com.sawwere.titlecounter.backend.app.storage.entity.GameEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.GameGenre;
import com.sawwere.titlecounter.backend.app.storage.entity.GameStatisticsAggregationResult;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.GameDeveloperRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameEntryRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameGenreRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GamePlatformRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.specification.GameSpecification;
import com.sawwere.titlecounter.common.dto.game.GameDeveloperDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryRequestDto;
import feign.FeignException;
import jakarta.annotation.Nullable;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Optional;
import java.util.logging.Logger;
import java.util.stream.Stream;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;


@Service
@RequiredArgsConstructor
public class GameService {
    private static final Logger LOGGER =
            Logger.getLogger(GameService.class.getName());
    private static final String IMAGE_STORE_PATH = "games/%d";

    private final GameRepository gameRepository;
    private final GamePlatformRepository gamePlatformRepository;
    private final GameGenreRepository gameGenreRepository;
    private final GameDeveloperRepository gameDeveloperRepository;
    private final GameEntryRepository gameEntryRepository;

    private final GameMapper gameDtoFactory;
    private final GameEntryDtoFactory gameEntryDtoFactory;

    private final ImageStorageService imageStorageService;
    private final UserService userService;
    private final ExternalContentSearchService externalContentSearchService;

    public Optional<Game> findGame(Long gameId) {
        return gameRepository.findById(gameId);
    }

    public Game findGameOrElseThrowException(Long gameId) throws NotFoundException {
        return gameRepository.findById(gameId)
                .orElseThrow(() -> new NotFoundException(String.format("Game with id '%s' doesn't exist", gameId))
                );
    }

    public GameEntry findGameEntryOrElseThrowException(Long gameEntryId) throws NotFoundException {
        return gameEntryRepository.findById(gameEntryId)
                .orElseThrow(() -> new NotFoundException(
                        String.format("GameEntry with id '%s' doesn't exist", gameEntryId))
                );
    }

    public GameDeveloper createGameDeveloper(GameDeveloperDto dto) {
        GameDeveloper gameDeveloperEntity = GameDeveloper.builder()
                .name(dto.getName())
                .build();
        gameDeveloperEntity = gameDeveloperRepository.save(gameDeveloperEntity);
        LOGGER.severe("Add new developer '%s'".formatted(dto.getName()));
        return gameDeveloperEntity;
    }

    @Transactional
    public Game createGame(@Valid GameCreationDto dto) {
        Game game = gameDtoFactory.creationDtoToEntity(dto);
        for (String platform : dto.getPlatforms()) {
            var platforms = gamePlatformRepository.findByName(platform);
            platforms.ifPresentOrElse(
                    game.getPlatforms()::add,
                    () -> LOGGER.severe("Not found platform '%s'".formatted(platform))
            );
        }
        for (String genre : dto.getGenres()) {
            var genres = gameGenreRepository.findByNameIgnoreCase(genre);
            genres.ifPresentOrElse(
                    game.getGenres()::add,
                    () -> {
                        GameGenre gameGenreEntity = GameGenre.builder().name(genre).build();
                        gameGenreEntity = gameGenreRepository.save(gameGenreEntity);
                        LOGGER.severe("Add new genre '%s'".formatted(genre));
                        game.getGenres().add(gameGenreEntity);
                    }
            );
        }
        if (dto.getDeveloper() != null) {
            for (String developer : dto.getDeveloper().split(",")) {
                String developerName = developer.trim();
                var developers = gameDeveloperRepository.findByNameIgnoreCase(developerName);
                developers.ifPresentOrElse(
                        game.getDevelopers()::add,
                        () -> {
                            game.getDevelopers().add(createGameDeveloper(
                                    GameDeveloperDto.builder()
                                            .name(developerName)
                                            .build())
                            );
                        }
                );
            }
        }
        gameRepository.save(game);
        if (!trySaveImage(game.getId(), dto)) {
            game.setHasImage(false);
            gameRepository.save(game);
        }

        LOGGER.info("Created game '%s' with id '%d' hltbId '%s'"
                .formatted(game.getTitle(), game.getId(), game.getExternalId().getHltbId())
        );
        return game;
    }

    @Transactional
    public Game updateGame(Long gameId, @Valid GameCreationDto gameDto) {
        gameDto.setId(gameId);
        Game game = findGameOrElseThrowException(gameId);
        game.setTitle(gameDto.getTitle());
        game.setGameType(gameDto.getGameType());
        game.setTime(gameDto.getTime());
        game.setDateRelease(gameDto.getDateRelease());
        game.setGlobalScore(gameDto.getGlobalScore());
        game.setDeveloper(game.getDeveloper());
        game.setDescription(gameDto.getDescription());
        game.getExternalId().setHltbId(gameDto.getExternalId().getHltbId());
        game.getExternalId().setSteamId(gameDto.getExternalId().getSteamId());
        game.setPlatforms(new ArrayList<>());
        for (String platform : gameDto.getPlatforms()) {
            var platforms = gamePlatformRepository.findByName(platform);
            platforms.ifPresentOrElse(
                    game.getPlatforms()::add,
                    () -> LOGGER.severe("Not found platform '%s'".formatted(platform))
            );
        }
        game.setGenres(new ArrayList<>());
        for (String genre : gameDto.getGenres()) {
            var genres = gameGenreRepository.findByNameIgnoreCase(genre);
            genres.ifPresentOrElse(
                    game.getGenres()::add,
                    () -> LOGGER.severe("Not found genre '%s'".formatted(genre))
            );
        }
        game.setDevelopers(new ArrayList<>());
        if (gameDto.getDeveloper() != null) {
            for (String developer : gameDto.getDeveloper().split(",")) {
                String developerName = developer.trim();
                var developers = gameDeveloperRepository.findByNameIgnoreCase(developerName);
                developers.ifPresentOrElse(
                        game.getDevelopers()::add,
                        () -> {
                            game.getDevelopers().add(createGameDeveloper(
                                    GameDeveloperDto.builder()
                                            .name(developerName)
                                            .build())
                            );
                        }
                );
            }
        }

        gameRepository.save(game);
        LOGGER.info("Updated game '%s' with id '%d'".formatted(gameDto.getTitle(), gameDto.getId()));
        return game;
    }

    @Transactional
    public void deleteGame(Long gameId) throws NotFoundException {
        Game game = findGameOrElseThrowException(gameId);
        gameRepository.deleteById(gameId);
        LOGGER.info("Deleted game '%s' with id '%d'".formatted(game.getTitle(), gameId));
    }

    @SuppressWarnings("checkstyle:ParameterNumber")
    @Transactional(readOnly = true)
    public Page<Game> search(@Nullable String title,
                             @Nullable Long timeFrom,
                             @Nullable Long timeTo,
                             @Nullable Float scoreFrom,
                             @Nullable Float scoreTo,
                             @Nullable LocalDate releaseAfter,
                             @Nullable LocalDate releaseBefore,
                             @Nullable String gameType,
                             int page, int pageSize) {
        Specification<Game> filter = Specification.where(null);
        if (title != null) {
            filter = filter.and(GameSpecification.titleContains(title));
        }
        if (timeFrom != null) {
            filter = filter.and(GameSpecification.timeGreaterThan(timeFrom));
        }
        if (timeTo != null) {
            filter = filter.and(GameSpecification.timeLessThan(timeTo));
        }
        if (scoreFrom != null) {
            filter = filter.and(GameSpecification.scoreGreaterThan(scoreFrom));
        }
        if (scoreTo != null) {
            filter = filter.and(GameSpecification.scoreLessThan(scoreTo));
        }
        if (releaseAfter != null) {
            filter = filter.and(GameSpecification.releaseFrom(releaseAfter));
        }
        if (releaseBefore != null) {
            filter = filter.and(GameSpecification.releaseFrom(releaseBefore));
        }

        Pageable pageable = PageRequest.of(page, pageSize);
        return gameRepository.findAll(filter, pageable);
    }

    public GameEntry createGameEntry(String username, GameEntryRequestDto gameEntryDto) {
        GameEntry gameEntry = gameEntryDtoFactory.dtoToEntity(gameEntryDto);
        Game gameEntity = findGameOrElseThrowException(gameEntryDto.getGameId());
        gameEntry.setGame(gameEntity);
        gameEntry.setCustomTitle(gameEntity.getTitle());

        User user = userService.findUserByUsername(username);
        gameEntry.setUser(user);
        gameEntryRepository.save(gameEntry);
        LOGGER.info("Created GameEntry %d for user %s".formatted(gameEntry.getId(), username));
        return gameEntry;
    }

    @Transactional
    public GameEntry updateGameEntry(long gameEntryId,
                                @Valid GameEntryRequestDto gameEntryDto,
                                @NotNull String authenticationName) {
        User user = userService.findUserByUsername(authenticationName);
        if (!user.getId().equals(gameEntryDto.getUserId())) {
            throw new ForbiddenException();
        }
        if (!Objects.equals(gameEntryId, gameEntryDto.getId())) {
            throw new IllegalArgumentException("Invalid id passed");
        }
        GameEntry gameEntry = findGameEntryOrElseThrowException(gameEntryId);
        gameEntry.setCustomTitle(gameEntryDto.getCustomTitle());
        gameEntry.setNote(gameEntryDto.getNote());
        gameEntry.setPlatform(gameEntryDto.getPlatform());
        gameEntry.setScore(gameEntryDto.getScore());
        gameEntry.setStatus(gameEntryDto.getStatus());
        gameEntry.setTime(gameEntryDto.getTime());
        gameEntry.setDateCompleted(gameEntryDto.getDateCompleted());
        gameEntryRepository.save(gameEntry);
        LOGGER.info("Updated GameEntry %d for user %s".formatted(gameEntry.getId(), user.getUsername()));
        return gameEntry;
    }

    @Transactional
    public void deleteGameEntry(Long gameEntryId, String authenticationName) throws NotFoundException {
        GameEntry gameEntry = findGameEntryOrElseThrowException(gameEntryId);
        User user = userService.findUserByUsername(authenticationName);
        if (!user.getId().equals(gameEntry.getUser().getId())) {
            throw new ForbiddenException();
        }
        gameEntryRepository.delete(gameEntry);
        LOGGER.info("Deleted GameEntry %d for user %s".formatted(gameEntry.getId(), user.getUsername()));
    }

    @Transactional(readOnly = true)
    public List<GameEntry> findGameEntriesByUser(String username) {
        User user = userService.findUserByUsername(username);
        Stream<GameEntry> gameEntries = gameEntryRepository.streamAllByUserId(user.getId());
        return gameEntries.toList();
    }

    @Transactional(readOnly = true)
    public List<GameEntry> findGameEntriesByUser(Long userId) {
        User user = userService.findUserOrElseThrowException(userId);
        Stream<GameEntry> gameEntries = gameEntryRepository.streamAllByUserId(user.getId());
        return gameEntries.toList();
    }

    public List<GameStatisticsAggregationResult> getStatistics() {
        return gameRepository.getStatistics();
    }

    public void updateStatistics() {
        for (GameStatisticsAggregationResult ar : getStatistics()) {
            Game game = findGameOrElseThrowException(ar.getGameId());
            game.setTime(ar.getTime());
            game.setGlobalScore(ar.getGlobalScore());
            gameRepository.save(game);
        }
    }

    public void autoUpdateGame(long startId, int limit) {
        for (long i = startId; i < startId + limit; i++) {
            gameRepository.findById(i).ifPresent(game -> {
                    var list = externalContentSearchService.findGames(null, game.getExternalId().getHltbId());
                    GameCreationDto dto = list.getContents().getFirst();
                    updateGame(game.getId(), dto);
                });
        }
    }

    public void autoCreateGame(int startId, int limit) {
        for (int i = startId; i < startId + limit; i++) {
            if (gameRepository.findByExternalId_HltbId(String.valueOf(i)).isEmpty()) {
                try {
                    var list = externalContentSearchService.findGames(null, String.valueOf(i));
                    if (list.getTotal() == 0) {
                        throw new NotFoundException(String.valueOf(i));
                    }
                    GameCreationDto dto = list.getContents().getFirst();
                    autoCreateHelper(dto);
                } catch (FeignException.FeignClientException ex) {
                    LOGGER.info("NOT FOUND hltbId '%d'"
                            .formatted(i)
                    );
                }
            }
        }
    }

    @Transactional
    public void autoCreateGame(String title) {
        var list = externalContentSearchService.findGames(title, null);
        if (list.getTotal() == 0) {
            throw new NotFoundException(title);
        }
        GameCreationDto dto = list.getContents().getFirst();
        autoCreateHelper(dto);
    }

    private void autoCreateHelper(GameCreationDto dto) {
        Optional<Game> existing = gameRepository.findByExternalId_HltbId(dto.getExternalId().getHltbId());
        if (existing.isEmpty()) {
            createGame(dto);
        } else {
            LOGGER.info("Game '%s' already exists with id '%d' "
                    .formatted(dto.getTitle(), existing.get().getId())
            );
        }
    }

    private boolean trySaveImage(long gameId, GameCreationDto dto) {
        try {
            if (dto.getImage() != null) {
                imageStorageService.store(dto.getImage(), IMAGE_STORE_PATH.formatted(gameId));
            } else {
                var image = externalContentSearchService.findImage(dto.getImageUrl());
                imageStorageService.store(image, IMAGE_STORE_PATH.formatted(gameId));
            }
            return true;
        } catch (StorageException storageException) {
            LOGGER.info("Couldn't save image for game '%s' with id '%d'. Check image file."
                    .formatted(dto.getTitle(), gameId)
            );
        } catch (Exception ex) {
            LOGGER.info("Couldn't load image for game '%s' with id '%d'"
                    .formatted(dto.getTitle(), gameId)
            );
        }
        return false;
    }
}
