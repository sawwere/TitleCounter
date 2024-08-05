package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GameEntry;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface GameEntryRepository extends JpaRepository<GameEntry, Long> {

    Stream<GameEntry> streamAllBy();

    Stream<GameEntry> streamAllByUserId(Long userId);
}
