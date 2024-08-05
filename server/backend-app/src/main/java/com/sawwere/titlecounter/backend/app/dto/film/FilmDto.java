package com.sawwere.titlecounter.backend.app.dto.film;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.time.LocalDate;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class FilmDto {
    private long id;

    @NotBlank
    private String title;

    @JsonProperty(value = "rus_title")
    private String rusTitle;

    @JsonProperty(value = "link_url")
    private String linkUrl;

    @NotNull
    @Min(0)
    @JsonProperty(value = "time")
    private Long time;

    @JsonProperty(value = "date_release")
    private LocalDate dateRelease;

    @NotNull
    @Min(0)
    @JsonProperty(value = "global_score")
    private Float globalScore;
}
