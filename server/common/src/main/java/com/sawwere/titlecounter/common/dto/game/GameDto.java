package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import java.time.LocalDate;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;


@Schema(description = "Game entity")
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameDto {
    @Schema(accessMode = Schema.AccessMode.READ_ONLY)
    private long id;

    @Schema(description = "Title of the game")
    @NotBlank
    private String title;

    @Schema(description = "Type of entity", example = "game")
    @NotBlank
    @JsonProperty("game_type")
    private String gameType;

    @Schema(description = "Short description of the game")
    @JsonProperty("description")
    private String description;

    @Schema(description = "List of external id's")
    @NotNull
    @JsonProperty("external_id")
    private GameExternalIdDto externalId;

    @Schema(description = "List of platforms on which the game was released")
    @NotNull
    private List<GamePlatformDto> platforms;

    @Schema(description = "List of developers who made the game")
    @NotNull
    private List<GameDeveloperDto> developers;

    @Schema(description = "Global time")
    @Min(0)
    private Long time;

    @Schema(description = "Release date of the game", example = "2024-01-24")
    @JsonProperty("date_release")
    private LocalDate dateRelease;

    @Schema(description = "Global ssore")
    @Min(0)
    @JsonProperty("global_score")
    private Float globalScore;
}
