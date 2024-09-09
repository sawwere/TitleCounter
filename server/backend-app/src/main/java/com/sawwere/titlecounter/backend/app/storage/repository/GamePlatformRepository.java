package com.sawwere.titlecounter.backend.app.storage.repository;

import com.sawwere.titlecounter.backend.app.storage.entity.GamePlatform;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface GamePlatformRepository extends JpaRepository<GamePlatform, Long> {
    Optional<GamePlatform> findByName(String name);
}
