package com.TitleCounter.DataAccess;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.servlet.ModelAndView;

import java.util.List;

@SpringBootApplication
public class DataAccessApplication
{
	public static void main(String[] args)
	{
		SpringApplication.run(DataAccessApplication.class, args);
	}

}
