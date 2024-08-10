package com.sawwere.titlecounter.notification.controller;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.DeliverCallback;
import com.sawwere.titlecounter.common.dto.game.GameEntryResponseDto;
import com.sawwere.titlecounter.notification.service.ApiClient;
import lombok.RequiredArgsConstructor;
import org.apache.commons.lang.NotImplementedException;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.io.IOException;
import java.security.Principal;
import java.util.List;
import java.util.concurrent.TimeoutException;

@RequiredArgsConstructor
@RestController()
@RequestMapping("/notification")
public class NotificationController {
    private final ApiClient gameEntriesClient;

    /**
     * @param principal
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
