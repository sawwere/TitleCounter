package com.TitleCounter.DataAccess;

import com.TitleCounter.DataAccess.config.FileStorageConfig;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.cloud.openfeign.EnableFeignClients;

@SpringBootApplication
@EnableConfigurationProperties(FileStorageConfig.class)
@EnableFeignClients
public class DataAccessApplication
{
	public static void main(String[] args)
	{
		SpringApplication.run(DataAccessApplication.class, args);
	}

}
