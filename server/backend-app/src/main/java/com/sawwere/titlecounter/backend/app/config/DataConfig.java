package com.sawwere.titlecounter.backend.app.config;

import com.sawwere.titlecounter.backend.app.service.DefaultFileStorageService;
import com.sawwere.titlecounter.backend.app.service.ImageStorageService;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;


@Configuration
@RequiredArgsConstructor
public class DataConfig {
    private static final Logger LOGGER =
            Logger.getLogger(DataConfig.class.getName());

    @Bean
    public CommandLineRunner dataLoader(DefaultFileStorageService storageService,
                                        ImageStorageService imageStorageService) {
        storageService.init();
        imageStorageService.init();

        return (args) -> {
            for (String arg : args) {
                LOGGER.info(arg);
            }
        };
    }
}
