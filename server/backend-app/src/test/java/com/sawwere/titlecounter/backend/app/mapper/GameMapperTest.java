package com.sawwere.titlecounter.backend.app.mapper;

import com.sawwere.titlecounter.backend.app.BasicTestContainerTest;
import com.sawwere.titlecounter.backend.app.dto.mapper.GameMapper;
import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.repository.GameRepository;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ActiveProfiles;

import java.time.LocalDate;
import java.time.LocalDateTime;

import static org.assertj.core.api.Assertions.assertThat;

@ActiveProfiles("test")
public class GameMapperTest extends BasicTestContainerTest {
    @Autowired
    private GameRepository gameRepository;
    @Autowired
    private GameMapper gameMapper;
    @Test
    public void shouldMapFilmToDto() {
        //given
        Game game = Game.builder()
                .id(1L)
                .title("title")
                .description("desc")
                .time(15)
                .dateRelease(LocalDate.EPOCH)
                .createdAt(LocalDateTime.now())
                .updatedAt(LocalDateTime.now())
                .build();

        //when
        GameDto gameDto = gameMapper.entityToDto( game );

        //result
        assertThat( gameDto ).isNotNull();
        assertThat( gameDto.getTitle() ).isEqualTo( "title" );
        assertThat( gameDto.getDescription() ).isEqualTo( "desc" );
        assertThat( gameDto.getDateRelease() ).isEqualTo( LocalDate.EPOCH );
    }
}
