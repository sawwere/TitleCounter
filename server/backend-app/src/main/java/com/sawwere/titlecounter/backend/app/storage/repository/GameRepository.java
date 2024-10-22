package com.sawwere.titlecounter.backend.app.storage.repository;


import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import com.sawwere.titlecounter.backend.app.storage.entity.GameStatisticsAggregationResult;
import java.util.List;
import java.util.Optional;
import java.util.stream.Stream;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.jpa.repository.Query;


public interface GameRepository extends JpaRepository<Game, Long>, JpaSpecificationExecutor<Game> {
    Stream<Game> streamAllBy();

    @SuppressWarnings("checkstyle:MethodName")
    Optional<Game> findByExternalId_HltbId(String hltbId);

    @Query("""
            SELECT new com.sawwere.titlecounter.backend.app.storage.entity.GameStatisticsAggregationResult(
                CAST(game.id AS long),
                CAST(avg(game_entry.score) AS float),
                CAST(avg(game_entry.time) AS long)
                )
            FROM GameEntry game_entry
            LEFT JOIN Game game ON game.id=game_entry.game.id
            WHERE  game_entry.status = 'completed'
            GROUP BY game.id
            HAVING COUNT(*) > 0
            ORDER BY game.id""")
    List<GameStatisticsAggregationResult> getStatistics();
}
