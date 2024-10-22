package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import com.sawwere.titlecounter.backend.app.storage.entity.FilmStatisticsAggregationResult;
import java.util.List;
import java.util.Optional;
import java.util.stream.Stream;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.jpa.repository.Query;


public interface FilmRepository extends JpaRepository<Film, Long>, JpaSpecificationExecutor<Film> {
    Stream<Film> streamAllBy();

    @SuppressWarnings("checkstyle:MethodName")
    Optional<Film> findByExternalId_KpId(String kpId);

    @SuppressWarnings("checkstyle:MethodName")
    Optional<Film> findByExternalId_TmdbId(String tmdbId);

    @Query("""
            SELECT new com.sawwere.titlecounter.backend.app.storage.entity.FilmStatisticsAggregationResult(
                CAST(film.id AS long),
                CAST(avg(film_entry.score) AS float)
                )
            FROM FilmEntry film_entry
            LEFT JOIN Film film ON film.id=film_entry.film.id
            WHERE  film_entry.status = 'completed'
            GROUP BY film.id
            HAVING COUNT(*) > 0
            ORDER BY film.id""")
    List<FilmStatisticsAggregationResult> getStatistics();
}
