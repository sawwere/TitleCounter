package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.game.GameEntryResponseDto;
import java.util.List;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

@FeignClient(name = "title-counter-api")
public interface ApiClient {
    @GetMapping("/api/users/{username}/games")
    List<GameEntryResponseDto> findAllGameEntries(@PathVariable (name = "username") String username);
}
