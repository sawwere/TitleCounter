package com.TitleCounter.DataAccess.storage.repository;

import com.TitleCounter.DataAccess.storage.entity.Film;
import com.TitleCounter.DataAccess.storage.entity.Game;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface FilmRepository extends JpaRepository<Film, Long> {
    Stream<Film> streamAllBy();

    Stream<Film> streamAllByTitle(String title);
}
