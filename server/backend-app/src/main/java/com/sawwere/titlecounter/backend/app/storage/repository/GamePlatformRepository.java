package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GamePlatform;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GamePlatformRepository extends JpaRepository<GamePlatform, Long> {
    Optional<GamePlatform> findByName(String name);
}
