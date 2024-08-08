package com.sawwere.titlecounter.backend.app.controller.api;

import com.sawwere.titlecounter.backend.app.dto.user.UserDtoFactory;
import com.sawwere.titlecounter.backend.app.service.UserService;
import com.sawwere.titlecounter.common.dto.role.RoleDto;
import com.sawwere.titlecounter.common.dto.user.UserDto;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@RequiredArgsConstructor
@RestController("api-userController")
public class UserController {
    public static final String FIND_USER = "/api/users/{user_id}";
    public static final String CURRENT_USER = "/api/user";
    public static final String FIND_ALL = "/api/users";
    public static final String DELETE_USER = "/api/users/{user_id}";
    public static final String ADD_ROLE = "/api/users/{user_id}/roles";

    private final UserService userService;

    private final UserDtoFactory userDtoFactory;
    @GetMapping(FIND_USER)
    public UserDto find(@PathVariable(value = "user_id") Long userId) {
        return userDtoFactory.entityToDto(userService.findUserById(userId));
    }

    @GetMapping(CURRENT_USER)
    public UserDto currentUser() {
        return userDtoFactory.entityToDto(userService.getCurrentUser());
    }

    @GetMapping(FIND_ALL)
    public List<UserDto> findAll() {
        return userService.allUsers().stream().map(userDtoFactory::entityToDto).toList();
    }

    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping(FIND_USER)
    public void deleteUser(@PathVariable(value = "user_id") Long userId) {
        userService.deleteUser(userId);
    }

    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PostMapping(ADD_ROLE)
    public void addRole(@RequestPart(name = "user_id") Long userId, @RequestBody RoleDto roleDto) {
        userService.addRole(userId, roleDto);
    }
}
