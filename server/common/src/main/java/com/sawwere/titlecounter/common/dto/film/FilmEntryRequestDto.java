package com.sawwere.titlecounter.common.dto.film;

import com.fasterxml.jackson.annotation.JsonProperty;
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

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class FilmEntryRequestDto {
    private Long id;

    @NotBlank
    @Size(min = 1, max = 64)
    @JsonProperty(value = "custom_title")
    private String customTitle;

    @Size(max = 512)
    private String note;

    @Min(1)
    @Max(10)
    private Integer score;

    @NotBlank
    private String status;

    @JsonProperty(value = "date_completed")
    private LocalDate dateCompleted;

    @NotNull
    @JsonProperty(value = "user_id")
    private Long userId;

    @NotNull
    @JsonProperty(value = "film_id")
    private Long filmId;
}
