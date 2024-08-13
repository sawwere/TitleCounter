package com.sawwere.titlecounter.frontend.app;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.openfeign.EnableFeignClients;

@SpringBootApplication
@EnableFeignClients
public class TitleCounterFrontendApplication
{
	public static void main(String[] args)
	{
		SpringApplication.run(TitleCounterFrontendApplication.class, args);
	}

}
