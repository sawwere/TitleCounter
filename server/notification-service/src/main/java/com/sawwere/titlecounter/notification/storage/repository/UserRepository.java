package com.sawwere.titlecounter.notification.storage.repository;


import com.sawwere.titlecounter.notification.storage.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByUsername(String username);
    Optional<User> findByEmail(String email);
}
