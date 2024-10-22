package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GameEntry;
import java.util.stream.Stream;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GameEntryRepository extends JpaRepository<GameEntry, Long> {

    Stream<GameEntry> streamAllBy();

    Stream<GameEntry> streamAllByUserId(Long userId);
}
