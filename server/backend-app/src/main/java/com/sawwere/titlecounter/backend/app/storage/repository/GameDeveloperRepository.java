package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GameDeveloper;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface GameDeveloperRepository extends JpaRepository<GameDeveloper, Long> {
    Optional<GameDeveloper> findByNameIgnoreCase(String name);

}
