package com.sawwere.titlecounter.common.dto.game;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;


@Schema(description = "Game platform entity")
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GamePlatformDto {
    @Schema(accessMode = Schema.AccessMode.READ_ONLY)
    @NotNull
    private long id;

    @Schema(description = "Name of the platform", example = "PC")
    @NotBlank
    private String name;
}
