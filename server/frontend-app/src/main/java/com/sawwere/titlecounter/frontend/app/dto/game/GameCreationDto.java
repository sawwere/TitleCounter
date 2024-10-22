package com.sawwere.titlecounter.frontend.app.dto.game;

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
import org.springframework.web.multipart.MultipartFile;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameCreationDto {
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

    private MultipartFile image;
}
