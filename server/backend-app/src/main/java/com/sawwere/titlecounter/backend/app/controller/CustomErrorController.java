package com.sawwere.titlecounter.backend.app.controller;

import com.sawwere.titlecounter.common.exception.ErrorInfo;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.web.error.ErrorAttributeOptions;
import org.springframework.boot.web.servlet.error.ErrorAttributes;
import org.springframework.boot.web.servlet.error.ErrorController;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.context.request.WebRequest;


/**
 * Служит для перехвата возникающих ошибок
 */
@RequiredArgsConstructor
@Controller
public class CustomErrorController implements ErrorController {
    private static final Logger LOGGER =
            Logger.getLogger(CustomErrorController.class.getName());
    private static final String ERROR_PATH = "/error";
    private final ErrorAttributes errorAttributes;

    /**
     * Перехватывает возникающие ошибки
     * @param webRequest текущий запрос
     * @return ResponseEntity с телом, содержашим информацию об ошибке в виде ErrorInfo
     */
    @RequestMapping(CustomErrorController.ERROR_PATH)
    public ResponseEntity<ErrorInfo> error(WebRequest webRequest) {
        var attributes = errorAttributes.getErrorAttributes(webRequest,
                ErrorAttributeOptions.of(
                        ErrorAttributeOptions.Include.ERROR,
                        ErrorAttributeOptions.Include.MESSAGE,
                        ErrorAttributeOptions.Include.STATUS)
        );
        Integer status = (Integer) attributes.get("status");
        LOGGER.info(status.toString());
        return ResponseEntity
                .status(status)
                .body(ErrorInfo.builder()
                        .error((String) attributes.get("error"))
                        .description((String) attributes.get("message"))
                        .build()
                );
    }
}
