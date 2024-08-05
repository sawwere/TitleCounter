package com.sawwere.titlecounter.backend.app;

import com.sawwere.titlecounter.backend.app.config.FileStorageConfig;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.cloud.openfeign.EnableFeignClients;

@SpringBootApplication
@EnableConfigurationProperties(FileStorageConfig.class)
@EnableFeignClients
public class TitleCounterBackendApplication
{
	public static void main(String[] args)
	{
		SpringApplication.run(TitleCounterBackendApplication.class, args);
	}

}
