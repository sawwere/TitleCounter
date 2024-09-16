package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.time.LocalDate;
import java.util.List;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameDto {
    private long id;

    @NotBlank
    private String title;

    @NotBlank
    @JsonProperty("game_type")
    private String gameType;

    @JsonProperty("description")
    private String description;

    @NotNull
    @JsonProperty("external_id")
    private GameExternalIdDto externalId;

    @NotNull
    private List<GamePlatformDto> platforms;

    @NotNull
    private List<GameDeveloperDto> developers;

    @Min(0)
    private Long time;

    @JsonProperty("date_release")
    private LocalDate dateRelease;

    @Min(0)
    @JsonProperty("global_score")
    private Float globalScore;
}
