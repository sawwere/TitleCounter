package com.sawwere.titlecounter.backend.app.config;

import org.springframework.amqp.core.Binding;
import org.springframework.amqp.core.BindingBuilder;
import org.springframework.amqp.core.DirectExchange;
import org.springframework.amqp.core.Queue;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
@ConditionalOnProperty(prefix = "app.rabbitmq", name = "enabled", havingValue = "true")
public class RabbitConfig {
    @Value("${app.rabbitmq.queue.name}")
    private String notificationQueue;

    @Value("${app.rabbitmq.exchange.name}")
    private String directExchange;

    @Value("${app.rabbitmq.routing.key}")
    private String routingKey;

    @Bean
    Queue notificationQueue() {
        return new Queue(notificationQueue, false);
    }

    @Bean
    DirectExchange directExchange() {
        return new DirectExchange(directExchange, false, false);
    }

    @Bean
    Binding binding(Queue queue, DirectExchange exchange) {
        return BindingBuilder.bind(queue)
                .to(exchange).with(routingKey);
    }

    @Bean
    SimpleMessageListenerContainer container(
            ConnectionFactory connectionFactory) {
        SimpleMessageListenerContainer container = new SimpleMessageListenerContainer();
        container.setConnectionFactory(connectionFactory);
        return container;
    }
}
