package com.TitleCounter.DataAccess.dto;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.validation.constraints.*;
import lombok.*;

import java.time.LocalDate;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameEntryDto {
    private Long id;

    @Size(max=255)
    private String note;

    @Min(0)
    @Max(10)
    private Long score;

    @NotBlank
    private String status;

    @JsonProperty(value = "date_completed")
    private LocalDate dateCompleted;

    @Min(0)
    private Long time;

    private String platform;

    @NotNull
    @JsonProperty(value = "client_id")
    private Long userId;

    @NotNull
    @JsonProperty(value = "game_id")
    private Long gameId;
}
