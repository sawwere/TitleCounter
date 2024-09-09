package com.sawwere.titlecounter.common.dto.game;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.util.List;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GamePlatformDto {
    @NotNull
    private long id;

    @NotBlank
    private String name;
}
