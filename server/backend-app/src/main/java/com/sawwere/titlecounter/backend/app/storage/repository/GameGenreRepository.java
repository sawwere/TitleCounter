package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GameGenre;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GameGenreRepository extends JpaRepository<GameGenre, Long> {
    Optional<GameGenre> findByNameIgnoreCase(String name);
}
