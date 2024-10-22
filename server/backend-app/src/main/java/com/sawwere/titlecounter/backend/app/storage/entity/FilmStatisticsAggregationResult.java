package com.sawwere.titlecounter.backend.app.storage.entity;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class FilmStatisticsAggregationResult {
    private long filmId;

    private Float globalScore;

    @Override
    public String toString() {
        return "FilmStatisticsAggregationResult{"
                + "filmId=" + filmId
                + ", globalScore=" + globalScore
                + '}';
    }
}
