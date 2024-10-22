package com.sawwere.titlecounter.backend.app.dto.film;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.sawwere.titlecounter.common.dto.film.FilmExternalIdDto;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
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
public class FilmCreationDto {
    @NotBlank
    private String title;

    @JsonProperty(value = "ru_title")
    private String ruTitle;

    @JsonProperty(value = "en_title")
    private String enTitle;

    @JsonProperty("external_id")
    private FilmExternalIdDto externalId;

    @JsonProperty("description")
    private String description;

    @Min(0)
    private Integer time;

    @JsonProperty(value = "year_release")
    private Integer yearRelease;

    @JsonProperty(value = "date_release")
    private LocalDate dateRelease;

    @Min(0)
    @JsonProperty(value = "global_score")
    private Float globalScore;

    @JsonProperty(value = "image_url")
    private String imageUrl;
}
