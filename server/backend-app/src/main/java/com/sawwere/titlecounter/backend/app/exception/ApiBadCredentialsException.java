package com.sawwere.titlecounter.backend.app.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(HttpStatus.UNAUTHORIZED)
public class ApiBadCredentialsException extends RuntimeException {
    public ApiBadCredentialsException(String message) {
        super(message);
    }
}
