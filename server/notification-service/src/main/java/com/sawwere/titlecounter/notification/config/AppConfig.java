package com.sawwere.titlecounter.notification.config;

import com.sawwere.titlecounter.notification.service.*;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.mail.javamail.JavaMailSender;
import org.thymeleaf.spring6.SpringTemplateEngine;

@Configuration
public class AppConfig {
//    @Bean
//    NotificationService notificationService(
//            @Value("${app.notification.email.enabled}") boolean isEmailEnabled,
//            @Value("${app.notification.logger.enabled}") boolean isLoggerEnabled,
//            EmailService emailService) {
//        NotificationDecorator notificator = new NotificationDecorator((x)->{});
//        if (isEmailEnabled) {
//            notificator = new EmailNotificator(notificator, emailService);
//        }
//
//        if (isLoggerEnabled)
//            notificator = new LoggerNotificator(notificator);
//        return notificator;
//    }
}
