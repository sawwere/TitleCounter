package com.sawwere.titlecounter.common.dto.user;

import com.sawwere.titlecounter.common.dto.role.RoleDto;
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
