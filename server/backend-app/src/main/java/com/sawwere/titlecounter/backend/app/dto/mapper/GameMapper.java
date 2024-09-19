package com.sawwere.titlecounter.backend.app.dto.mapper;

import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import org.springframework.stereotype.Component;

@Component
public interface GameMapper {
    GameDto entityToDto(Game game);

    Game dtoToEntity(GameDto gameDto);

    Game creationDtoToEntity(GameCreationDto gameDto);
}
