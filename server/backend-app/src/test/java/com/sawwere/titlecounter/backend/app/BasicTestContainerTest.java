package com.sawwere.titlecounter.backend.app;

import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.DynamicPropertyRegistry;
import org.springframework.test.context.DynamicPropertySource;
import org.testcontainers.containers.PostgreSQLContainer;
import org.testcontainers.junit.jupiter.Testcontainers;

@Testcontainers
public class BasicTestContainerTest {
    public static PostgreSQLContainer<?> POSTGRES;

    static {
        POSTGRES = new PostgreSQLContainer<>("postgres:16")
                .withDatabaseName("TitleCounter-test")
                .withUsername("TestUser")
                .withPassword("1234");
        POSTGRES.start();
    }

    @DynamicPropertySource
    static void jdbcProperties(DynamicPropertyRegistry registry) {
        registry.add("spring.datasource.url", POSTGRES::getJdbcUrl);
        registry.add("spring.datasource.username", POSTGRES::getUsername);
        registry.add("spring.datasource.password", POSTGRES::getPassword);
//        registry.add("spring.flyway.url", POSTGRES::getJdbcUrl);
//        registry.add("spring.flyway.username", POSTGRES::getUsername);
//        registry.add("spring.flyway.password", POSTGRES::getPassword);
    }
}
