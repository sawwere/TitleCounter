package com.sawwere.titlecounter.notification.storage.repository;

import com.sawwere.titlecounter.notification.storage.entity.Role;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface RoleRepository extends JpaRepository<Role, Long> {
    Optional<Role> findByName(String name);
}
