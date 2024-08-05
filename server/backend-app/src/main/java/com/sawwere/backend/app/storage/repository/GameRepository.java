package com.sawwere.backend.app.storage.repository;

import com.sawwere.backend.app.storage.entity.Game;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.stream.Stream;

public interface GameRepository extends JpaRepository<Game, Long>, JpaSpecificationExecutor<Game> {
    Stream<Game> streamAllBy();
}
