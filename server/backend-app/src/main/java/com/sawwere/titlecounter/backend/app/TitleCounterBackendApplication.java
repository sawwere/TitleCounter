package com.sawwere.titlecounter.backend.app;

import com.sawwere.titlecounter.backend.app.config.FileStorageConfig;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.cloud.openfeign.EnableFeignClients;
import org.springframework.scheduling.annotation.EnableScheduling;

@SpringBootApplication
@EnableConfigurationProperties(FileStorageConfig.class)
@EnableFeignClients
@EnableScheduling
public class TitleCounterBackendApplication {
    public static void main(String[] args) {
        SpringApplication.run(TitleCounterBackendApplication.class, args);
    }
}
