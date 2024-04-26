package com.TitleCounter.DataAccess.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(HttpStatus.BAD_REQUEST)
public class ApiBadCredentialsException extends RuntimeException {
    public ApiBadCredentialsException(String message) {
        super(message);
    }
}
