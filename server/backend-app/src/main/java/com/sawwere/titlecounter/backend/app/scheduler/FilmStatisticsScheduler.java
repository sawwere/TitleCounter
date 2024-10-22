package com.sawwere.titlecounter.backend.app.scheduler;

import com.sawwere.titlecounter.backend.app.service.FilmService;
import java.util.logging.Logger;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;



@Component
@RequiredArgsConstructor
@ConditionalOnProperty(name = "app.scheduler.enabled")
public class FilmStatisticsScheduler {
    private static final Logger LOGGER =
            Logger.getLogger(GameStatisticsScheduler.class.getName());

    private final FilmService filmService;

    @Scheduled(fixedRateString = "${app.scheduler.interval}")
    public void updateStatistics() {
        LOGGER.info("Starting update film statistics");
        filmService.updateStatistics();
        LOGGER.info("Finish update film statistics");
    }
}
