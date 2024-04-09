package com.TitleCounter.DataAccess.storage.repository;

import com.TitleCounter.DataAccess.storage.entity.GameEntry;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.stream.Stream;

public interface GameEntryRepository extends JpaRepository<GameEntry, Long> {

    Stream<GameEntry> streamAllBy();

    Stream<GameEntry> streamAllByUserId(Long userId);
}
