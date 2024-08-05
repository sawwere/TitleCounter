package com.sawwere.titlecounter.backend.app.dto;

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
public class GameDto {
    private long id;

    @NotBlank
    private String title;

    @JsonProperty("link_url")
    private String linkUrl;

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
