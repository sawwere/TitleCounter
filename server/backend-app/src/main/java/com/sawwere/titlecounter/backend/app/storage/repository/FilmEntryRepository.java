package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.FilmEntry;
import java.util.stream.Stream;
import org.springframework.data.jpa.repository.JpaRepository;

public interface FilmEntryRepository extends JpaRepository<FilmEntry, Long> {

    Stream<FilmEntry> streamAllBy();

    Stream<FilmEntry> streamAllByUserId(Long userId);
}
