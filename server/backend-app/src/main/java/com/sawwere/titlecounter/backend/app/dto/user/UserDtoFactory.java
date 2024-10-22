package com.sawwere.titlecounter.backend.app.dto.user;

import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.common.dto.role.RoleDto;
import com.sawwere.titlecounter.common.dto.user.UserDto;
import org.springframework.stereotype.Component;

@Component
public class UserDtoFactory {
    public UserDto entityToDto(User user) {
        return  UserDto.builder()
                .id(user.getId())
                .username(user.getUsername())
                .email(user.getEmail())
                .roles(user.getRoles().stream().map(x -> new RoleDto(x.getName())).toList())
                .build();
    }
}
