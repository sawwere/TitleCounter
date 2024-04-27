package com.TitleCounter.DataAccess.dto;

import com.TitleCounter.DataAccess.storage.entity.Game;
import org.springframework.stereotype.Component;

@Component
public class GameDtoFactory {
    public GameDto entityToDto(Game game) {
        return  GameDto.builder()
                .id(game.getId())
                .title(game.getTitle())
                .linkUrl(game.getLinkUrl())
                .time(game.getTime())
                .dateRelease(game.getDateRelease())
                .globalScore(game.getGlobalScore())
                .build();
    }

    public Game dtoToEntity(GameDto gameDto) {
        return Game.builder()
                .id(gameDto.getId())
                .title(gameDto.getTitle())
                .linkUrl(gameDto.getLinkUrl())
                .time(gameDto.getTime())
                .dateRelease(gameDto.getDateRelease())
                .globalScore(gameDto.getGlobalScore())
                .build();
    }
}
