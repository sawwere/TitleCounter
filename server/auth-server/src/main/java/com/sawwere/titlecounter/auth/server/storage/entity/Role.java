package com.sawwere.titlecounter.auth.server.storage.entity;

import jakarta.persistence.*;
import lombok.*;
import org.springframework.security.core.GrantedAuthority;

import java.util.Set;

@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "roles")
public class Role implements GrantedAuthority {
    @Id
    private Long id;

    @Column(nullable = false)
    private String name;

    @Override
    public String getAuthority() {
        return getName();
    }
}
