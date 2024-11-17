package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.event.UserRegisteredEventDto;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.stereotype.Component;

@RequiredArgsConstructor
@Component
@ConditionalOnProperty(name = "app.notification.logger.enabled")
public class LoggerNotificator implements NotificationService {
    private static final Logger LOGGER = Logger.getLogger(LoggerNotificator.class.getName());

    @Override
    public void sendGreeting(UserRegisteredEventDto dto) {
        LOGGER.info(String.format("Sending greeting notification to %s", dto.getUser().getUsername()));
    }
}
