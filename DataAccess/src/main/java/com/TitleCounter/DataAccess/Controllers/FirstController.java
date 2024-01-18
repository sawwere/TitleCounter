package com.TitleCounter.DataAccess.Controllers;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class FirstController
{
    @GetMapping("/")
    public String index() {
        return "Hello, user";
    }
}
