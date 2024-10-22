package com.sawwere.titlecounter.notification.config;

import org.springframework.context.annotation.Configuration;

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
