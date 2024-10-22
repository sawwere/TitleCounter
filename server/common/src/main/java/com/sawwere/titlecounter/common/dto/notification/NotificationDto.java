package com.sawwere.titlecounter.common.dto.notification;

import com.sawwere.titlecounter.common.dto.user.UserDto;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

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
