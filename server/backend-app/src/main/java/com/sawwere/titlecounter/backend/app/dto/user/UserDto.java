package com.sawwere.titlecounter.backend.app.dto.user;

import com.sawwere.titlecounter.backend.app.dto.role.RoleDto;
import lombok.*;

import java.util.List;

@Builder
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class UserDto {
    private Long id;
    private String username;
    private String email;
    private List<RoleDto> roles;
}
