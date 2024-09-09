package com.sawwere.titlecounter.common.dto.film;

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

    @JsonProperty(value = "ru_title")
    private String ruTitle;

    @JsonProperty(value = "en_title")
    private String enTitle;

    @JsonProperty("external_id")
    private FilmExternalIdDto externalId;

    @JsonProperty("description")
    private String description;

    @Min(0)
    @JsonProperty(value = "time")
    private Integer time;

    @JsonProperty(value = "year_release")
    private Integer yearRelease;

    @JsonProperty(value = "date_release")
    private LocalDate dateRelease;

    @Min(0)
    @JsonProperty(value = "global_score")
    private Float globalScore;
}
