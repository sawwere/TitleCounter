package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.film.FilmCreationDto;
import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.storage.entity.*;
import com.sawwere.titlecounter.backend.app.storage.repository.*;
import com.sawwere.titlecounter.backend.app.storage.repository.specification.GameSpecification;
import com.sawwere.titlecounter.backend.app.dto.game.GameDtoFactory;
import com.sawwere.titlecounter.backend.app.dto.game.GameEntryDtoFactory;
import com.sawwere.titlecounter.backend.app.exception.ForbiddenException;
import com.sawwere.titlecounter.backend.app.exception.NotFoundException;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryRequestDto;
import feign.FeignException;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Optional;
import java.util.concurrent.TimeUnit;
import java.util.logging.Logger;
import java.util.stream.Stream;

@Service
@RequiredArgsConstructor
public class GameService {
    private static final Logger logger =
            Logger.getLogger(GameService.class.getName());

    private final GameRepository gameRepository;
    private final GamePlatformRepository gamePlatformRepository;
    private final GameGenreRepository gameGenreRepository;
    private final GameDeveloperRepository gameDeveloperRepository;
    private final GameEntryRepository gameEntryRepository;

    private final GameDtoFactory gameDtoFactory;
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
        logger.info("Created game '%s' with id '%d'".formatted(game.getTitle(), game.getId()));
        return game;
    }

    //TODO
    @Transactional
    public Game updateGame(Long gameId, @Valid GameDto gameDto) {
        gameDto.setId(gameId);
        logger.info("Updated game '%s' with id '%d'".formatted(gameDto.getTitle(), gameDto.getId()));
        return gameRepository.save(gameDtoFactory.dtoToEntity(gameDto));
    }

    @Transactional
    public void deleteGame(Long gameId) {
        Game game = findGameOrElseThrowException(gameId);
        gameRepository.deleteById(gameId);
        logger.info("Deleted game '%s' with id '%d'".formatted(game.getTitle(), gameId));
    }

    @Transactional(readOnly=true)
    public List<Game> findAll() {
        return gameRepository.streamAllBy().toList();
    }

    @Transactional(readOnly=true)
    public List<Game> search(Optional<String> query) {
        Specification<Game> filter = Specification.where(null);
        if (query.isPresent()) {
            filter = filter.and(GameSpecification.titleContains(query.get()));
        }
        return gameRepository.findAll(filter);
    }

    public GameEntry createGameEntry(String username, GameEntryRequestDto gameEntryDto) {
        GameEntry gameEntry = gameEntryDtoFactory.dtoToEntity(gameEntryDto);
        Game gameEntity = findGameOrElseThrowException(gameEntryDto.getGameId());
        gameEntry.setGame(gameEntity);
        gameEntry.setCustomTitle(gameEntity.getTitle());

        User user = userService.findUserByUsername(username);
        gameEntry.setUser(user);
        gameEntryRepository.save(gameEntry);
        logger.info("Created GameEntry %d for user %s".formatted(gameEntry.getId(), username));
        return gameEntry;
    }

    @Transactional
    public void updateGameEntry(Long gameEntryId, @Valid GameEntryRequestDto gameEntryDto, Authentication authentication) {
        User user = userService.findUserByUsername(authentication.getName());
        if (!user.getId().equals(gameEntryDto.getUserId()))
            throw new ForbiddenException("You don't have access to requested resource");
        if (!Objects.equals(gameEntryId, gameEntryDto.getId()))
            throw new IllegalArgumentException("Invalid id passed");
        GameEntry gameEntry = findGameEntryOrElseThrowException(gameEntryId);
        gameEntry.setCustomTitle(gameEntryDto.getCustomTitle());
        gameEntry.setNote(gameEntryDto.getNote());
        gameEntry.setPlatform(gameEntryDto.getPlatform());
        gameEntry.setScore(gameEntryDto.getScore());
        gameEntry.setStatus(gameEntryDto.getStatus());
        gameEntry.setTime(gameEntryDto.getTime());
        gameEntry.setDateCompleted(gameEntryDto.getDateCompleted());
        gameEntryRepository.save(gameEntry);
        logger.info("Updated GameEntry %d for user %s".formatted(gameEntry.getId(), user.getUsername()));
    }

    @Transactional
    public void deleteGameEntry(Long gameEntryId, Authentication authentication) {
        GameEntry gameEntry = findGameEntryOrElseThrowException(gameEntryId);
        User user = userService.findUserByUsername(authentication.getName());
        if (!user.getId().equals(gameEntry.getUser().getId()))
            throw new ForbiddenException("You don't have access to requested resource");
        gameEntryRepository.delete(gameEntry);
        logger.info("Deleted GameEntry %d for user %s".formatted(gameEntry.getId(), user.getUsername()));
    }

    @Transactional(readOnly=true)
    public List<GameEntry> findGameEntriesByUser(String username) {
        User user = userService.findUserByUsername(username);
        Stream<GameEntry> gameEntries = gameEntryRepository.streamAllByUserId(user.getId());
        return gameEntries.toList();
    }

    @Transactional(readOnly=true)
    public List<Game> findGamesByUser(String username) {
        User user = userService.findUserByUsername(username);
        List<GameEntry> gameEntries = findGameEntriesByUser(username);
        return gameEntries.stream().map(GameEntry::getGame).toList();
    }

    public void autoCreateGame(int startId, int limit) {
        for (int i = startId; i < startId + limit; i++) {
            if (gameRepository.findByExternalId_HltbId(String.valueOf(i)).isEmpty())
                try {
                    TimeUnit.MILLISECONDS.sleep((int)(Math.random() * 1000));
                    var list = externalContentSearchService.findGames(null, String.valueOf(i));
                    if (list.getTotal() == 0)
                        throw new NotFoundException(String.valueOf(i));
                    GameCreationDto dto = list.getContents().get(0);
                    if (gameRepository.findByExternalId_HltbId(dto.getExternalId().getHltbId()).isEmpty()) {
                        Game game = gameDtoFactory.creationDtoToEntity(dto);
                        for (String platform : dto.getPlatforms()) {
                            var searchRes = gamePlatformRepository.findByName(platform);
                            searchRes.ifPresentOrElse(
                                    game.getPlatforms()::add,
                                    () -> logger.severe("Not found platform '%s'".formatted(platform))
                            );
                        }
                        for (String genre : dto.getGenres()) {
                            var searchRes = gameGenreRepository.findByNameIgnoreCase(genre);
                            searchRes.ifPresentOrElse(
                                    game.getGenres()::add,
                                    () -> {
                                        GameGenre gameGenreEntity = GameGenre.builder().name(genre).build();
                                        gameGenreEntity = gameGenreRepository.save(gameGenreEntity);
                                        logger.severe("Add new genre '%s'".formatted(genre));
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
                                            logger.severe("Add new developer '%s'".formatted(developerName));
                                            game.getDevelopers().add(gameDeveloperEntity);
                                        }
                                );
                            }
                        }

                        gameRepository.save(game);
                        System.out.println(dto.getImageUrl());
                        var image = externalContentSearchService.findImage(dto.getImageUrl());
                        imageStorageService.store(image, "games/%d".formatted(game.getId()));
                        logger.info("Created game '%s' with id '%d' hltbId '%s'"
                                .formatted(game.getTitle(), game.getId(), game.getExternalId().getHltbId())
                        );
                    }
                } catch (FeignException.FeignClientException ex) {
                    logger.info("NOT FOUND hltbId '%d'"
                            .formatted(i)
                    );
                } catch (InterruptedException ex) {
                    logger.severe("Interrupted at id '%d'".formatted(i));
                    break;
                }
        }
    }

    @Transactional
    public void autoCreateGame(String title) {
        var list = externalContentSearchService.findGames(title, null);
        if (list.getTotal() == 0)
            throw new NotFoundException(title);
        GameCreationDto dto = list.getContents().get(0);
        if (gameRepository.findByExternalId_HltbId(dto.getExternalId().getHltbId()).isEmpty()) {
            Game game = gameDtoFactory.creationDtoToEntity(dto);
            gameRepository.save(game);
            System.out.println(dto.getImageUrl());
            var image = externalContentSearchService.findImage(dto.getImageUrl());
            imageStorageService.store(image, "games/%d".formatted(game.getId()));
            logger.info("Created game '%s' with id '%d' hltbId '%s'"
                    .formatted(game.getTitle(), game.getId(), game.getExternalId().getHltbId())
            );
        }
    }
}
