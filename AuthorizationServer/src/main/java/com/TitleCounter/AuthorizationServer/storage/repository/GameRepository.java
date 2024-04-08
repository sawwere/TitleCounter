package com.TitleCounter.AuthorizationServer.storage.repository;

import com.TitleCounter.AuthorizationServer.storage.entity.Game;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface GameRepository extends JpaRepository<Game, Long> {

    Stream<Game> streamAllBy();
}
