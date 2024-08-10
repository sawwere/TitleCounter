package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.user.UserDto;
import lombok.RequiredArgsConstructor;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Service;

@RequiredArgsConstructor
@Service
public class RabbitConsumer {
    private final NotificationService emailNotificator;

    @RabbitListener(queues = {"${app.rabbitmq.queue.name}"})
    public void receive(UserDto dto) {
        emailNotificator.sendGreeting(dto);
    }
}
