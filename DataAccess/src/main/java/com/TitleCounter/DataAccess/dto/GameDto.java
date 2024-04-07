package com.TitleCounter.DataAccess.dto;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.persistence.Column;
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
public class GameDto {
    private long id;

    @NotBlank
    private String title;

    @JsonProperty("image_url")
    private String imageUrl;

    @JsonProperty("link_url")
    private String link_url;

    @NotNull
    @Min(0)
    private Long time;

    @JsonProperty("date_release")
    private LocalDate dateRelease;

    @NotNull
    @Min(0)
    @JsonProperty("global_score")
    private Float globalScore;
}
