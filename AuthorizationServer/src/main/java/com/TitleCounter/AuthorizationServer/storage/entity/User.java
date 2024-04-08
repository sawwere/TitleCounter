package com.TitleCounter.AuthorizationServer.storage.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.Size;
import lombok.*;
import org.hibernate.annotations.Cascade;
import org.hibernate.annotations.CascadeType;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.List;
import java.util.Set;

@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "users")
public class User implements UserDetails {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Size(min = 1, message = "Не меньше 1 знаков")
    private String username;

    @Size(min = 1, message = "Не меньше 1 знаков")
    private String password;

    @Email
    private String email;

    @ManyToMany(fetch = FetchType.EAGER)
    @Cascade(CascadeType.REMOVE)
    private List<Role> roles;

    @OneToMany(orphanRemoval = true)
    @JoinColumn(name = "user_id", referencedColumnName = "id")
    private List<GameEntry> gameEntry;

    @OneToMany(orphanRemoval = true)
    @JoinColumn(name = "user_id", referencedColumnName = "id")
    private List<FilmEntry> filmEntry;

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return getRoles();
    }

    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    @Override
    public boolean isAccountNonLocked() {
        return true;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    @Override
    public boolean isEnabled() {
        return true;
    }
}
