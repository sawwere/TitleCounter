package com.sawwere.titlecounter.backend.app.config;

import com.sawwere.titlecounter.common.exception.ErrorInfo;
import io.swagger.v3.core.converter.ModelConverters;
import io.swagger.v3.oas.annotations.OpenAPIDefinition;
import io.swagger.v3.oas.annotations.enums.SecuritySchemeType;
import io.swagger.v3.oas.annotations.info.Info;
import io.swagger.v3.oas.annotations.security.SecurityScheme;
import io.swagger.v3.oas.annotations.servers.Server;
import java.util.List;
import org.springdoc.core.customizers.OpenApiCustomizer;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;


@Configuration
@OpenAPIDefinition(
        info = @Info(
                title = "TitleCounter Api",
                description = "TitleCounter application", version = "0.0.1"
        )
)
@SecurityScheme(
        name = "bearerAuth",
        type = SecuritySchemeType.HTTP,
        bearerFormat = "JWT",
        scheme = "Bearer"
)
public class OpenApiConfig {
    @Server
    @Bean
    public List<io.swagger.v3.oas.models.servers.Server> servers(@Value("app.publicUrl") String url) {
        io.swagger.v3.oas.models.servers.Server server = new io.swagger.v3.oas.models.servers.Server();
        server.url(url);
        server.description("Main api");
        return List.of(server);
    }

    @Bean
    public OpenApiCustomizer openApiCustomizer() {
        return openApi -> {
            //Получаем схему ошибки
            var sharedErrorSchema = ModelConverters.getInstance()
                    .read(ErrorInfo.class).get(ErrorInfo.class.getSimpleName());
            if (sharedErrorSchema == null) {
                throw new IllegalStateException(
                        "Can't generate response for 4xx and 5xx errors");
            }

            //Добавляем тело ответа ко всем ответам с кодами 4xx и 5xx
            openApi.getPaths().values().forEach(pathItem -> pathItem.readOperations().forEach(operation ->
                    operation.getResponses().forEach((status, response) -> {
                        if (status.startsWith("4") || status.startsWith("5")) {
                            response.getContent().forEach((code, mediaType) -> mediaType.setSchema(sharedErrorSchema));
                        }
                    })));
        };
    }
}
