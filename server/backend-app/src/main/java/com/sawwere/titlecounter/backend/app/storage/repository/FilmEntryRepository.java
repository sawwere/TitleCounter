package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.FilmEntry;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface FilmEntryRepository extends JpaRepository<FilmEntry, Long> {

    Stream<FilmEntry> streamAllBy();

    Stream<FilmEntry> streamAllByUserId(Long userId);
}
