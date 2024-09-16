package com.sawwere.titlecounter.common.dto.game;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameDeveloperDto {
    @NotNull
    private long id;

    @NotBlank
    private String name;
}
