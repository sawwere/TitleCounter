package com.sawwere.titlecounter.backend.app.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

/**
 * Exception generated when attempting to create entity with fields violating unique constraints
 */
@ResponseStatus(HttpStatus.NOT_FOUND)
public class AlreadyExistsException extends RuntimeException {
    public AlreadyExistsException(String message) {
        super(message);
    }
}
