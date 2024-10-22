package com.sawwere.titlecounter.frontend.app.dto.film;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
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

    @JsonProperty(value = "rus_title")
    private String rusTitle;

    @JsonProperty(value = "link_url")
    private String linkUrl;

    @NotNull
    @Min(0)
    private Long time;

    @JsonProperty(value = "date_release")
    private LocalDate dateRelease;

    @NotNull
    @Min(0)
    @JsonProperty(value = "global_score")
    private Float globalScore;

    @JsonProperty(value = "image_url")
    private String imageUrl;
}
