package com.sawwere.titlecounter.frontend.app.service;

import com.sawwere.titlecounter.common.dto.game.GameDto;
import com.sawwere.titlecounter.common.dto.game.GameEntryResponseDto;
import com.sawwere.titlecounter.common.dto.user.UserDto;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@FeignClient("title-counter-api")
public interface ApiClient {
    @GetMapping("/api/games/{game_id}")
    GameDto findGame(@PathVariable(value = "game_id") Long gameId);

    @GetMapping("/api/games")
    public List<GameDto> findAllGames(@RequestParam(value = "q", required = false) String query);

    @GetMapping("/api/games/{game_id}")
    List<GameEntryResponseDto> findGameEntriesByUser(@PathVariable(name="username") String username);

    @GetMapping("/api/users/{user_id}")
    UserDto findUserById(@PathVariable(value = "user_id") Long userId);

    @GetMapping("/api/users}")
    UserDto findUserByUsername(@RequestParam(value = "username") String username);

    @GetMapping("/api/users")
    public List<UserDto> findAllUsers();
}
