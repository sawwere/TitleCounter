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
public class FilmCreationDto {
    @NotBlank
    private String title;

    @JsonProperty(value = "alternative_title")
    private String alternativeTitle;

    @JsonProperty("imdb_id")
    private String imdbIdd;

    @JsonProperty("kp_id")
    private String kpId;

    @Min(0)
    private Long time;

    @JsonProperty(value = "date_release")
    private LocalDate dateRelease;

    @Min(0)
    @JsonProperty(value = "global_score")
    private Float globalScore;

    @JsonProperty(value = "image_url")
    private String imageUrl;
}
