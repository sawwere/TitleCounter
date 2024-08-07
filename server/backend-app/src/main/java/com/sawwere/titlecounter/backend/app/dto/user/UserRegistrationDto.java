package com.sawwere.titlecounter.backend.app.dto.user;

import com.sawwere.titlecounter.backend.app.util.PasswordMatches;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@PasswordMatches
public class UserRegistrationDto {
    @NotNull
    @NotBlank
    private String username;

    @NotNull
    @NotBlank
    private String password;

    private String passwordConfirm;

    @NotNull
    @Email
    private String email;
}
