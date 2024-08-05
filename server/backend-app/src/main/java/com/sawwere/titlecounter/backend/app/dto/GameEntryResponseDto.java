package com.sawwere.titlecounter.backend.app.dto;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.validation.constraints.*;
import lombok.*;

import java.time.LocalDate;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameEntryResponseDto {
    private Long id;

    @Size(min = 1, max = 64)
    @JsonProperty(value = "custom_title")
    private String customTitle;

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
    @JsonProperty(value = "user_id")
    private Long userId;

    @NotNull
    private GameDto game;
}
