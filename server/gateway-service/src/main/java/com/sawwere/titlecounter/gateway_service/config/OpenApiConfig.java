package com.sawwere.titlecounter.gateway_service.config;

import io.swagger.v3.oas.annotations.OpenAPIDefinition;
import io.swagger.v3.oas.annotations.info.Info;
import java.util.HashSet;
import java.util.Set;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springdoc.core.models.GroupedOpenApi;
import org.springdoc.core.properties.AbstractSwaggerUiConfigProperties;
import org.springdoc.core.properties.SwaggerUiConfigProperties;
import org.springframework.cloud.gateway.route.RouteDefinitionLocator;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;


@Configuration
@OpenAPIDefinition(
        info = @Info(
                title = "TitleCounter Gateway",
                description = "TitleCounter gateway service", version = "0.0.1"
        )
)
@Slf4j
@RequiredArgsConstructor
public class OpenApiConfig {
        private static final String API_DOCS = "/v3/api-docs";

        private final RouteDefinitionLocator locator;

        @Bean
        public GroupedOpenApi apis(SwaggerUiConfigProperties swaggerUiConfigProperties) {

                Set<AbstractSwaggerUiConfigProperties.SwaggerUrl> urls = new HashSet<>();
                locator.getRouteDefinitions().subscribe(routeDefinition -> {
                        log.info("Discovered route definition: {}", routeDefinition.getId());
                        String resourceName = routeDefinition.getId();
                        if (resourceName.matches(".*-API")) {
                                String displayName = resourceName
                                        .replace("ReactiveCompositeDiscoveryClient_", "");
                                String location = routeDefinition.getPredicates()
                                        .getFirst()
                                        .getArgs()
                                        .get("pattern").replace("/**", API_DOCS);
                                log.info("Adding swagger resource: {} with location {}", resourceName, location);
                                urls.add(
                                        new AbstractSwaggerUiConfigProperties
                                                .SwaggerUrl(resourceName, location, displayName)
                                );
                        }

                });
                swaggerUiConfigProperties.setUrls(urls);
                return GroupedOpenApi.builder()
                        .group("api-gateway")
                        .pathsToMatch("/api/**")
                        .build();
        }
}
