package com.TitleCounter.DataAccess.storage.repository;

import com.TitleCounter.DataAccess.storage.entity.FilmEntry;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface FilmEntryRepository extends JpaRepository<FilmEntry, Long> {

    Stream<FilmEntry> streamAllBy();

    Stream<FilmEntry> streamAllByUserId(Long userId);
}
