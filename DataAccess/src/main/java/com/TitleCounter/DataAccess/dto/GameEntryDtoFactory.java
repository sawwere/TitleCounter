package com.TitleCounter.DataAccess.dto;

import com.TitleCounter.DataAccess.storage.entity.Game;
import com.TitleCounter.DataAccess.storage.entity.GameEntry;
import com.TitleCounter.DataAccess.storage.entity.User;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Component;

import java.time.LocalDate;

@RequiredArgsConstructor
@Component
public class GameEntryDtoFactory {
    private final GameDtoFactory gameDtoFactory;
    public GameEntryResponseDto entityToDto(GameEntry gameEntry) {
        return  GameEntryResponseDto.builder()
                .id(gameEntry.getId())
                .customTitle(gameEntry.getCustomTitle())
                .note(gameEntry.getNote())
                .score(gameEntry.getScore())
                .status(gameEntry.getStatus())
                .dateCompleted(gameEntry.getDateCompleted())
                .time(gameEntry.getTime())
                .platform(gameEntry.getPlatform())
                .game(gameDtoFactory.entityToDto(gameEntry.getGame()))
                .userId(gameEntry.getUser().getId())
                .build();
    }

    public GameEntry dtoToEntity(GameEntryCreationDto gameEntryDto) {
        return GameEntry.builder()
                .id(gameEntryDto.getId())
                .customTitle(gameEntryDto.getCustomTitle())
                .note(gameEntryDto.getNote())
                .score(gameEntryDto.getScore())
                .status(gameEntryDto.getStatus())
                .dateCompleted(gameEntryDto.getDateCompleted())
                .time(gameEntryDto.getTime())
                .platform(gameEntryDto.getPlatform())
                .game(Game.builder().id(gameEntryDto.getGameId()).build())
                .user(User.builder().id(gameEntryDto.getUserId()).build())
                .build();
    }

    public GameEntryCreationDto makeDefault(Long gameId, Long userId) {
        return  GameEntryCreationDto.builder()
                .score(0L)
                .status("COMPLETED")
                .dateCompleted(LocalDate.now())
                .time(0L)
                .gameId(gameId)
                .userId(userId)
                .build();
    }
}
