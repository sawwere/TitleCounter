package com.TitleCounter.DataAccess.service;

import com.TitleCounter.DataAccess.dto.GameDto;
import com.TitleCounter.DataAccess.dto.GameDtoFactory;
import com.TitleCounter.DataAccess.dto.GameEntryDto;
import com.TitleCounter.DataAccess.dto.GameEntryDtoFactory;
import com.TitleCounter.DataAccess.exception.NotFoundException;
import com.TitleCounter.DataAccess.storage.entity.Game;
import com.TitleCounter.DataAccess.storage.entity.GameEntry;
import com.TitleCounter.DataAccess.storage.entity.User;
import com.TitleCounter.DataAccess.storage.repository.GameEntryRepository;
import com.TitleCounter.DataAccess.storage.repository.GameRepository;
import jakarta.transaction.Transactional;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.logging.Logger;
import java.util.stream.Stream;

@Service
@RequiredArgsConstructor
public class GameService {
    private static final Logger logger =
            Logger.getLogger(GameService.class.getName());

    private final GameRepository gameRepository;
    private final GameEntryRepository gameEntryRepository;

    private final GameDtoFactory gameDtoFactory;
    private final GameEntryDtoFactory gameEntryDtoFactory;

    private final UserService userService;
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

    @Transactional
    public List<Game> findAll() {
        return gameRepository.streamAllBy().toList();
    }

    @Transactional
    public GameEntry createGameEntry(String username, GameEntryDto gameEntryDto) {
        GameEntry gameEntry = gameEntryDtoFactory.dtoToEntity(gameEntryDto);
        Game gameEntity = findGameOrElseThrowException(gameEntryDto.getGameId());
        gameEntry.setGame(gameEntity);

        User user = userService.findUserByUsername(username);
        gameEntry.setUser(user);
        gameEntryRepository.save(gameEntry);
        return gameEntry;
    }

    @Transactional
    public void deleteGameEntry(Long gameEntryId) {
        GameEntry gameEntry = findGameEntryOrElseThrowException(gameEntryId);
        gameEntryRepository.delete(gameEntry);
    }

    @Transactional
    public Stream<GameEntry> findGameEntriesByUser(String username) {
        User user = userService.findUserByUsername(username);
        Stream<GameEntry> gameEntries = gameEntryRepository.streamAllByUserId(user.getId());
        return gameEntries;
    }


}
