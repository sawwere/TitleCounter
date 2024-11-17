package com.sawwere.titlecounter.common.dto.event;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.sawwere.titlecounter.common.dto.user.UserDto;
import java.util.Locale;
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
public class UserRegisteredEventDto {
    private UserDto user;

    @JsonProperty(value = "confirmation_link")
    private String confirmationLink;

    private Locale locale;
}
