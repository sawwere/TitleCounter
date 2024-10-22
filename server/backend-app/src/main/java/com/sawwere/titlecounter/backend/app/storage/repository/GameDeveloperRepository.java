package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GameDeveloper;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GameDeveloperRepository extends JpaRepository<GameDeveloper, Long> {
    Optional<GameDeveloper> findByNameIgnoreCase(String name);

}
