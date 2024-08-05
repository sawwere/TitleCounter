package com.sawwere.titlecounter.backend.app.config;

import lombok.Getter;
import org.springframework.boot.context.properties.ConfigurationProperties;

@Getter
@ConfigurationProperties("storage-config")
public class FileStorageConfig {
    private final String baseLocation = "resources";
    private final String imageLocation = baseLocation + "/images";
}
