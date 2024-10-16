package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.sawwere.titlecounter.common.dto.game.GameDto;
import io.swagger.v3.oas.annotations.media.Schema;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Schema(description = "Result of searching for games")
@Builder
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class GameSearchResultDto {
    @Schema(description = "List of found games")
    private List<GameDto> contents;

    @Schema(description = "Total number of pages")
    @JsonProperty("total_pages")
    private Integer totalPages;
}
