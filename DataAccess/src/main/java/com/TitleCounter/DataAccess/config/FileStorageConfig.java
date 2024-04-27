package com.TitleCounter.DataAccess.config;

import lombok.Getter;
import org.springframework.boot.context.properties.ConfigurationProperties;

@Getter
@ConfigurationProperties("storage-config")
public class FileStorageConfig {
    private final String baseLocation = "resources";
    private final String imageLocation = baseLocation + "/images";
}
