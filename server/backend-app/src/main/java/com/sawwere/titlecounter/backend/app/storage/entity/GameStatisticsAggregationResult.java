package com.sawwere.titlecounter.backend.app.storage.entity;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class GameStatisticsAggregationResult {
    private long gameId;

    private Float globalScore;

    private Long time;

    @Override
    public String toString() {
        return "GameStatisticsAggregationResult{"
                + "gameId=" + gameId
                + ", globalScore=" + globalScore
                + ", time=" + time
                + '}';
    }
}
