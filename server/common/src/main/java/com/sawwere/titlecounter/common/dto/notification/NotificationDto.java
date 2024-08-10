package com.sawwere.titlecounter.common.dto.notification;

import com.sawwere.titlecounter.common.dto.user.UserDto;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class NotificationDto {
    private UserDto user;
    private String subject;
    private String text;
}
