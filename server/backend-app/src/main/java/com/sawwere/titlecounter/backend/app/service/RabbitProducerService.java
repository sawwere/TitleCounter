package com.sawwere.titlecounter.backend.app.service;

import lombok.RequiredArgsConstructor;
import org.jboss.logging.Logger;
import org.springframework.amqp.core.Message;
import org.springframework.amqp.core.MessageProperties;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.amqp.support.converter.MessageConverter;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@RequiredArgsConstructor
@Service
public class RabbitProducerService {
    private final Logger logger = Logger.getLogger(RabbitProducerService.class);

    private final RabbitTemplate rabbitTemplate;
    @Value("${app.rabbitmq.exchange.name}")
    private String exchange;

    @Value("${app.rabbitmq.routing.key}")
    private String routingKey;

    public void send(Object dto) {
        send(this.exchange, this.routingKey, dto);
    }

    public void send(String exchange, String routingKey, Object dto) {
        MessageConverter messageConverter = new Jackson2JsonMessageConverter();
        MessageProperties messageProperties = new MessageProperties();
        Message message = messageConverter.toMessage(dto, messageProperties);
        rabbitTemplate.send(exchange, routingKey, message);
        logger.info("send greeting to kafka");
    }
}
