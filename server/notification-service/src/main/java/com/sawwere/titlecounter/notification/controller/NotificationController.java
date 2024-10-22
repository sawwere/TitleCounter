package com.sawwere.titlecounter.notification.controller;

import com.sawwere.titlecounter.common.dto.game.GameEntryResponseDto;
import com.sawwere.titlecounter.notification.service.ApiClient;
import java.security.Principal;
import java.util.List;
import lombok.RequiredArgsConstructor;
import org.apache.commons.lang.NotImplementedException;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RequiredArgsConstructor
@RestController()
@RequestMapping("/notification")
public class NotificationController {
    private final ApiClient gameEntriesClient;

    /**
     * Notification settings for current user
     * @param principal User authentication object
     * @return Настройки уведомлений данного пользователя
     */
    @GetMapping(path = "/settings")
    public List<GameEntryResponseDto> getCurrentSettings(Principal principal) {
        return gameEntriesClient.findAllGameEntries("admin");
    }

    @PutMapping(path = "/settings")
    public Object update(Principal principal) {
        throw new NotImplementedException();
    }
}
