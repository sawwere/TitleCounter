package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import java.util.Optional;
import java.util.stream.Stream;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;


public interface GameRepository extends JpaRepository<Game, Long>, JpaSpecificationExecutor<Game> {
    Stream<Game> streamAllBy();

    @SuppressWarnings("checkstyle:MethodName")
    Optional<Game> findByExternalId_HltbId(String hltbId);
}
