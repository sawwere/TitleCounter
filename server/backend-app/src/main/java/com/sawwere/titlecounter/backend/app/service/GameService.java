package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.dto.game.GameEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.mapper.GameMapper;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GameDeveloper;
import com.sawwere.titlecounter.backend.app.storage.entity.GameEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.GameGenre;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.backend.app.storage.repository.GameDeveloperRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameEntryRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameGenreRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GamePlatformRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.GameRepository;
import com.sawwere.titlecounter.backend.app.storage.repository.specification.GameSpecification;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryRequestDto;
import feign.FeignException;
import jakarta.annotation.Nullable;
import jakarta.validation.Valid;

import java.time.LocalDate;
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
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;


@Service
@RequiredArgsConstructor
public class GameService {
    private static final Logger LOGGER =
            Logger.getLogger(GameService.class.getName());

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

    @Transactional
    public Game createGame(@Valid GameDto gameDto) {
        Game game = gameDtoFactory.dtoToEntity(gameDto);
        gameRepository.save(game);
        LOGGER.info("Created game '%s' with id '%d'".formatted(game.getTitle(), game.getId()));
        return game;
    }

    //TODO
    @Transactional
    public Game updateGame(Long gameId, @Valid GameDto gameDto) {
        gameDto.setId(gameId);
        LOGGER.info("Updated game '%s' with id '%d'".formatted(gameDto.getTitle(), gameDto.getId()));
        return gameRepository.save(gameDtoFactory.dtoToEntity(gameDto));
    }

    @Transactional
    public void deleteGame(Long gameId) throws NotFoundException {
        Game game = findGameOrElseThrowException(gameId);
        gameRepository.deleteById(gameId);
        LOGGER.info("Deleted game '%s' with id '%d'".formatted(game.getTitle(), gameId));
    }

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
    public void updateGameEntry(Long gameEntryId,
                                @Valid GameEntryRequestDto gameEntryDto,
                                Authentication authentication) {
        User user = userService.findUserByUsername(authentication.getName());
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
    }

    @Transactional
    public void deleteGameEntry(Long gameEntryId, Authentication authentication) throws NotFoundException {
        GameEntry gameEntry = findGameEntryOrElseThrowException(gameEntryId);
        User user = userService.findUserByUsername(authentication.getName());
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

    public void autoCreateGame(int startId, int limit) {
        for (int i = startId; i < startId + limit; i++) {
            if (gameRepository.findByExternalId_HltbId(String.valueOf(i)).isEmpty()) {
                try {
                    var list = externalContentSearchService.findGames(null, String.valueOf(i));
                    if (list.getTotal() == 0) {
                        throw new NotFoundException(String.valueOf(i));
                    }
                    GameCreationDto dto = list.getContents().get(0);
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
        GameCreationDto dto = list.getContents().get(0);
        autoCreateHelper(dto);
    }

    private void autoCreateHelper(GameCreationDto dto) {
        Optional<Game> existing = gameRepository.findByExternalId_HltbId(dto.getExternalId().getHltbId());
        if (existing.isEmpty()) {
            Game game = gameDtoFactory.creationDtoToEntity(dto);
            for (String platform : dto.getPlatforms()) {
                var searchRes = gamePlatformRepository.findByName(platform);
                searchRes.ifPresentOrElse(
                        game.getPlatforms()::add,
                        () -> LOGGER.severe("Not found platform '%s'".formatted(platform))
                );
            }
            for (String genre : dto.getGenres()) {
                var searchRes = gameGenreRepository.findByNameIgnoreCase(genre);
                searchRes.ifPresentOrElse(
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
                    var searchRes = gameDeveloperRepository.findByNameIgnoreCase(developerName);
                    searchRes.ifPresentOrElse(
                            game.getDevelopers()::add,
                            () -> {
                                GameDeveloper gameDeveloperEntity = GameDeveloper.builder()
                                        .name(developerName)
                                        .build();
                                gameDeveloperEntity = gameDeveloperRepository.save(gameDeveloperEntity);
                                LOGGER.severe("Add new developer '%s'".formatted(developerName));
                                game.getDevelopers().add(gameDeveloperEntity);
                            }
                    );
                }
            }

            gameRepository.save(game);
            LOGGER.info(dto.getImageUrl());
            var image = externalContentSearchService.findImage(dto.getImageUrl());
            imageStorageService.store(image, "games/%d".formatted(game.getId()));
            LOGGER.info("Created game '%s' with id '%d' hltbId '%s'"
                    .formatted(game.getTitle(), game.getId(), game.getExternalId().getHltbId())
            );
        } else {
            LOGGER.info("Game '%s' already exists with id '%d' "
                    .formatted(dto.getTitle(), existing.get().getId())
            );
        }
    }
}
