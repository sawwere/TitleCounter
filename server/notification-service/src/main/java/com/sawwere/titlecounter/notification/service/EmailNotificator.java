package com.sawwere.titlecounter.notification.service;

import com.sawwere.titlecounter.common.dto.user.UserDto;
import jakarta.mail.MessagingException;
import java.nio.charset.StandardCharsets;
import java.util.logging.Level;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.mail.MailException;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Component;
import org.thymeleaf.context.Context;
import org.thymeleaf.spring6.SpringTemplateEngine;

@RequiredArgsConstructor
@Component
@ConditionalOnProperty(name = "app.notification.email.enabled")
public class EmailNotificator implements NotificationService {
    private static final Logger LOGGER =
            Logger.getLogger(EmailNotificator.class.getName());

    private final JavaMailSender emailSender;

    private final SpringTemplateEngine templateEngine;

    @Override
    public void sendGreeting(UserDto dto) {
        try {
            var message = emailSender.createMimeMessage();
            MimeMessageHelper messageHelper = new MimeMessageHelper(message, StandardCharsets.UTF_8.name());
            Context context = new Context();
            context.setVariable("username", dto.getUsername());
            String text = templateEngine.process("registration-complete", context);
            messageHelper.setTo(dto.getEmail());
            messageHelper.setSubject("Finishing registration");
            messageHelper.setText(text, true);
            emailSender.send(message);
            LOGGER.log(Level.INFO, String.format("Sent email to %s: %s", dto.getEmail(), dto.getUsername()));
        } catch (MailException | MessagingException mailException) {
            LOGGER.log(Level.SEVERE, String.format("Error while sending email %s", mailException.getMessage()));
        }
    }
}
