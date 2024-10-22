package com.sawwere.titlecounter.auth.server.storage.repository;

import com.sawwere.titlecounter.auth.server.storage.entity.User;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByUsername(String username);

    User findByEmail(String email);
}
