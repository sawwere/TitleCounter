package com.sawwere.titlecounter.common.dto.user;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class UserLoginDto {
    @NotNull
    @NotBlank(message = "Имя пользователя не может быть пустыми")
    private String username;

    @NotNull
    @NotBlank(message = "Пароль не может быть пустыми")
    private String password;
}
