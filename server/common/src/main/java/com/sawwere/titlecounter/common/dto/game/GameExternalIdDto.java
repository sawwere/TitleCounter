package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;


@Schema(description = "Game external id entity")
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameExternalIdDto {
    @Schema(description = "Id of the game on howlongtobeat", accessMode = Schema.AccessMode.READ_ONLY)
    @JsonProperty("hltb_id")
    private String hltbId;

    @Schema(description = "Id of the game on steam", accessMode = Schema.AccessMode.READ_ONLY)
    @JsonProperty("steam_id")
    private String steamId;
}
