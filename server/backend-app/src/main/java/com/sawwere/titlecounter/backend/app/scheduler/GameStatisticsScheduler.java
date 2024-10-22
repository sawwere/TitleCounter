package com.sawwere.titlecounter.backend.app.scheduler;

import com.sawwere.titlecounter.backend.app.service.GameService;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;


@Component
@RequiredArgsConstructor
@ConditionalOnProperty(name = "app.scheduler.enabled")
public class GameStatisticsScheduler {
    private static final Logger LOGGER =
            Logger.getLogger(GameStatisticsScheduler.class.getName());

    private final GameService gameService;

    @Scheduled(fixedRateString = "${app.scheduler.interval}")
    public void updateStatistics() {
        LOGGER.info("Starting update game statistics");
        gameService.updateStatistics();
        LOGGER.info("Finish update game statistics");
    }
}
