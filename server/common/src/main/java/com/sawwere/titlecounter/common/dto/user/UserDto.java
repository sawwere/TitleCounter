package com.sawwere.titlecounter.common.dto.user;

import com.sawwere.titlecounter.common.dto.role.RoleDto;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

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
