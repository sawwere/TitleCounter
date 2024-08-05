package com.sawwere.backend.app.dto.user;

import com.sawwere.backend.app.storage.entity.User;
import org.springframework.stereotype.Component;

@Component
public class UserDtoFactory {
    public UserDto entityToDto(User user) {
        return  UserDto.builder()
                .id(user.getId())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();
    }

    public User dtoToEntity(UserDto userDto) {
        return User.builder()
                .id(userDto.getId())
                .username(userDto.getUsername())
                .email(userDto.getEmail())
                .build();
    }
}
