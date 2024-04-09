package com.TitleCounter.DataAccess.controller;

import com.TitleCounter.DataAccess.dto.UserDto;
import com.TitleCounter.DataAccess.dto.UserDtoFactory;
import com.TitleCounter.DataAccess.service.UserService;
import com.TitleCounter.DataAccess.storage.entity.User;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@RequiredArgsConstructor
@RestController
public class UserController {
    public static final String FIND = "api/users";
    public static final String FIND_BY_NAME = "api/users";
    public static final String CREATE_USER = "api/users";

    private final UserService userService;
    private final UserDtoFactory userDtoFactory;
    @GetMapping(FIND)
    private ResponseEntity<?> findByName(@RequestParam(value = "name") Optional<String> name) {
        if (name.isPresent()) {
            User userEntity = userService.findUserByUsername(name.get());
            return ResponseEntity.ofNullable(userDtoFactory.entityToDto(userEntity));
        }
        else {
            return ResponseEntity.ofNullable(userService.allUsers().stream().map(userDtoFactory::entityToDto).toList());
        }
    }

    @PostMapping(CREATE_USER)
    private UserDto createUser(@RequestBody UserDto userDto) {
        User userEntity = userDtoFactory.dtoToEntity(userDto);
        userService.saveUser(userEntity);
        return userDtoFactory.entityToDto(userEntity);
    }
}
