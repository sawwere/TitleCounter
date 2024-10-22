package com.sawwere.titlecounter.notification.storage.repository;


import com.sawwere.titlecounter.notification.storage.entity.User;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByUsername(String username);

    Optional<User> findByEmail(String email);
}
