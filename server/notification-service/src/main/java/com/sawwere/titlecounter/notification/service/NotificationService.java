package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.user.UserDto;

public interface NotificationService {
    void sendGreeting(UserDto dto);
}
