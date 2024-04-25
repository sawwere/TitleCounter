package com.TitleCounter.DataAccess.storage.repository;

import com.TitleCounter.DataAccess.storage.entity.Game;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface GameRepository extends JpaRepository<Game, Long> {
    Stream<Game> streamAllBy();

    Stream<Game> streamAllByTitle(String title);
}
