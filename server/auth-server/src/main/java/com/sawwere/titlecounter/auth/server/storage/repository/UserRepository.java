package com.sawwere.titlecounter.auth.server.storage.repository;

import com.sawwere.titlecounter.auth.server.storage.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByUsername(String username);
    User findByEmail(String email);
}
