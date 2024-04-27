package com.TitleCounter.DataAccess;

import com.TitleCounter.DataAccess.config.FileStorageConfig;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;

@SpringBootApplication
@EnableConfigurationProperties(FileStorageConfig.class)
public class DataAccessApplication
{
	public static void main(String[] args)
	{
		SpringApplication.run(DataAccessApplication.class, args);
	}

}
