package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameExternalIdDto {
    @JsonProperty("hltb_id")
    private String hltbId;
    @JsonProperty("steam_id")
    private String steamId;
}
