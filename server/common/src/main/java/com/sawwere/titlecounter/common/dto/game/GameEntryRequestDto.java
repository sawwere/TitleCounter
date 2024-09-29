package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import java.time.LocalDate;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;


@Schema(description = "Dto for request for game entry entity")
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameEntryRequestDto {
    @Schema(accessMode = Schema.AccessMode.READ_ONLY)
    private Long id;

    @Schema(description = "Game entry custom title given by user. Default value equals to game title itself")
    @Size(min = 1, max = 64)
    @JsonProperty(value = "custom_title")
    private String customTitle;

    @Schema(description = "User's note on the game")
    @Size(max = 512)
    private String note;

    @Schema(description = "User's score")
    @Min(1)
    @Max(10)
    private Integer score;

    @Schema(description = "Status of the entry", example = "completed")
    @NotBlank
    private String status;

    @Schema(description = "Date of completion of the game", example = "2024-01-24")
    @JsonProperty(value = "date_completed")
    private LocalDate dateCompleted;

    @Schema(description = "User's time in the game")
    @Min(0)
    private Long time;

    @Schema(description = "Platform on which game is played", deprecated = true)
    @Max(64)
    private String platform;

    @NotNull
    @JsonProperty(value = "user_id")
    private Long userId;

    @NotNull
    @JsonProperty(value = "game_id")
    private Long gameId;
}
