package com.sawwere.titlecounter.backend.app.dto;

import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GameEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
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

    public GameEntry dtoToEntity(GameEntryRequestDto gameEntryDto) {
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

    public GameEntryRequestDto makeDefault(Long gameId, Long userId) {
        return  GameEntryRequestDto.builder()
                .score(0L)
                .status("COMPLETED")
                .dateCompleted(LocalDate.now())
                .time(0L)
                .gameId(gameId)
                .userId(userId)
                .build();
    }
}
