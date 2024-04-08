package com.TitleCounter.AuthorizationServer.storage.repository;

import com.TitleCounter.AuthorizationServer.storage.entity.Role;
import com.TitleCounter.AuthorizationServer.storage.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface RoleRepository extends JpaRepository<Role, Long> {
    Optional<Role> findByName(String name);
}
