package com.sawwere.titlecounter.backend.app.dto.game;

import com.sawwere.titlecounter.backend.app.dto.mapper.GameMapper;
import com.sawwere.titlecounter.backend.app.storage.entity.GameDeveloper;
import com.sawwere.titlecounter.backend.app.storage.entity.GameExternalId;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GamePlatform;
import com.sawwere.titlecounter.common.dto.game.GameDeveloperDto;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import com.sawwere.titlecounter.common.dto.game.GameExternalIdDto;
import com.sawwere.titlecounter.common.dto.game.GamePlatformDto;
import org.springframework.stereotype.Component;

import java.util.ArrayList;

@Component
public class GameDtoFactory implements GameMapper {
    public GameDto entityToDto(Game game) {
        return  GameDto.builder()
                .id(game.getId())
                .title(game.getTitle())
                .gameType(game.getGameType())
                .developers(game.getDevelopers().stream().map(this::map).toList())
                .description(game.getDescription())
                .platforms(game.getPlatforms().stream().map(this::map).toList())
                .externalId(map(game.getExternalId()))
                .time(game.getTime())
                .dateRelease(game.getDateRelease())
                .globalScore(game.getGlobalScore())
                .build();
    }

    private GamePlatformDto map(GamePlatform obj) {
        return GamePlatformDto.builder()
                .id(obj.getId())
                .name(obj.getName())
                .build();
    }

    private GamePlatform map(GamePlatformDto obj) {
        return GamePlatform.builder()
                .id(obj.getId())
                .name(obj.getName())
                .build();
    }

    private GameExternalIdDto map(GameExternalId obj) {
        return GameExternalIdDto.builder()
                .hltbId(obj.getHltbId())
                .steamId(obj.getSteamId())
                .build();
    }

    private GameExternalId map(GameExternalIdDto obj) {
        return GameExternalId.builder()
                .hltbId(obj.getHltbId())
                .steamId(obj.getSteamId())
                .build();
    }

    private GameDeveloperDto map(GameDeveloper obj) {
        return GameDeveloperDto.builder()
                .id(obj.getId())
                .name(obj.getName())
                .build();
    }

    private GameDeveloper map(GameDeveloperDto obj) {
        return GameDeveloper.builder()
                .id(obj.getId())
                .name(obj.getName())
                .build();
    }

    public Game dtoToEntity(GameDto gameDto) {
        return Game.builder()
                .id(gameDto.getId())
                .title(gameDto.getTitle())
                .gameType(gameDto.getGameType())
                .developers(gameDto.getDevelopers().stream().map(this::map).toList())
                .description(gameDto.getDescription())
                .platforms(gameDto.getPlatforms().stream().map(this::map).toList())
                .externalId(map(gameDto.getExternalId()))
                .time(gameDto.getTime())
                .dateRelease(gameDto.getDateRelease())
                .globalScore(gameDto.getGlobalScore())
                .build();
    }

    /**
     * @param gameDto Dto to be mapped into entity
     * @return mapped object
     * @implNote WARN: Does not map list fields
     */
    public Game creationDtoToEntity(GameCreationDto gameDto) {
        return Game.builder()
                .title(gameDto.getTitle())
                .gameType(gameDto.getGameType())
                .developer(gameDto.getDeveloper())
                .description(gameDto.getDescription())
                .platforms(new ArrayList<>(4))
                .genres(new ArrayList<>())
                .developers(new ArrayList<>())
                .externalId(map(gameDto.getExternalId()))
                .time(gameDto.getTime())
                .dateRelease(gameDto.getDateRelease())
                .globalScore(gameDto.getGlobalScore())
                .build();
    }
}
