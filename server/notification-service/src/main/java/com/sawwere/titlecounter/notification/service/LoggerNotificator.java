package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.user.UserDto;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.stereotype.Component;

import java.util.logging.Logger;

@RequiredArgsConstructor
@Component
@ConditionalOnProperty(name="app.notification.logger.enabled")
public class LoggerNotificator implements NotificationService {
    private static final Logger logger = Logger.getLogger(LoggerNotificator.class.getName());

    @Override
    public void sendGreeting(UserDto dto) {
        logger.info(String.format("Sending greeting notification to %s", dto.getUsername()));
    }
}