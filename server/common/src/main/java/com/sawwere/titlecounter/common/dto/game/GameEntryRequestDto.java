package com.sawwere.titlecounter.common.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.validation.constraints.*;
import lombok.*;

import java.time.LocalDate;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameEntryRequestDto {
    private Long id;

    @Size(min = 1, max = 64)
    @JsonProperty(value = "custom_title")
    private String customTitle;

    @Size(max=512)
    private String note;

    @Min(1)
    @Max(10)
    private Long score;

    @NotBlank
    private String status;

    @JsonProperty(value = "date_completed")
    private LocalDate dateCompleted;

    @Min(0)
    private Long time;

    @Max(64)
    private String platform;

    @NotNull
    @JsonProperty(value = "user_id")
    private Long userId;

    @NotNull
    @JsonProperty(value = "game_id")
    private Long gameId;
}
