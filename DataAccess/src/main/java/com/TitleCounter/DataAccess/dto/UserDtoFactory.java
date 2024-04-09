package com.TitleCounter.DataAccess.dto;

import com.TitleCounter.DataAccess.storage.entity.Game;
import com.TitleCounter.DataAccess.storage.entity.User;
import org.springframework.stereotype.Component;

@Component
public class UserDtoFactory {
    public UserDto entityToDto(User user) {
        return  UserDto.builder()
                .id(user.getId())
                .name(user.getUsername())
                .email(user.getEmail())
                .build();
    }

    public User dtoToEntity(UserDto userDto) {
        return User.builder()
                .id(userDto.getId())
                .username(userDto.getName())
                .email(userDto.getEmail())
                .build();
    }
}
