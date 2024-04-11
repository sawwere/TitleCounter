package com.TitleCounter.DataAccess.dto;

import com.TitleCounter.DataAccess.storage.entity.Game;
import com.TitleCounter.DataAccess.storage.entity.GameEntry;
import com.TitleCounter.DataAccess.storage.entity.User;
import org.springframework.stereotype.Component;

import java.time.LocalDate;

@Component
public class GameEntryDtoFactory {
    public GameEntryDto entityToDto(GameEntry gameEntry) {
        return  GameEntryDto.builder()
                .id(gameEntry.getId())
                .note(gameEntry.getNote())
                .score(gameEntry.getScore())
                .status(gameEntry.getStatus())
                .dateCompleted(gameEntry.getDateCompleted())
                .time(gameEntry.getTime())
                .platform(gameEntry.getPlatform())
                .gameId(gameEntry.getId())
                .userId(gameEntry.getUser().getId())
                .build();
    }

    public GameEntry dtoToEntity(GameEntryDto gameEntryDto) {
        return GameEntry.builder()
                .id(gameEntryDto.getId())
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

    public GameEntryDto makeDefault(Long gameId, Long userId) {
        return  GameEntryDto.builder()
                .score(0L)
                .status("COMPLETED")
                .dateCompleted(LocalDate.now())
                .time(0L)
                .gameId(gameId)
                .userId(userId)
                .build();
    }
}
