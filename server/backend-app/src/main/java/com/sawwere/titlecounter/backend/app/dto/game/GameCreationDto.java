package com.sawwere.titlecounter.backend.app.dto.game;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.sawwere.titlecounter.common.dto.game.GameExternalIdDto;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;
import org.springframework.web.multipart.MultipartFile;

import java.time.LocalDate;
import java.util.List;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameCreationDto {
    @NotBlank
    private String title;

    @NotBlank
    @JsonProperty("game_type")
    private String gameType;

    @JsonProperty("developer")
    private String developer;

    @JsonProperty("description")
    private String description;

    @JsonProperty("external_id")
    private GameExternalIdDto externalId;

    @NotNull
    private List<String> platforms;

    @Min(0)
    private Long time;

    @JsonProperty("date_release")
    private LocalDate dateRelease;

    @Min(0)
    @JsonProperty("global_score")
    private Float globalScore;

    @JsonProperty(value = "image_url")
    private String imageUrl;

    private MultipartFile image;
}