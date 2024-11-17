package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.event.UserRegisteredEventDto;

public interface NotificationService {
    void sendGreeting(UserRegisteredEventDto dto);
}
