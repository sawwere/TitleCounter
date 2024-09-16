package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GameGenre;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface GameGenreRepository extends JpaRepository<GameGenre, Long> {
    Optional<GameGenre> findByNameIgnoreCase(String name);
}
