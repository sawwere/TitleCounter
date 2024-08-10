package com.sawwere.titlecounter.notification.storage.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.Size;
import lombok.*;
import org.hibernate.annotations.Cascade;
import org.hibernate.annotations.CascadeType;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;
import java.util.Objects;

@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "users", indexes = {
        @Index(name = "unique_username_idx", columnList = "username", unique = true)
})
public class User implements UserDetails {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "username", unique = true, nullable = false)
    @Size(min = 1, message = "Не меньше 1 знаков")
    private String username;

    @Column(name = "password", nullable = false)
    @Size(min = 1, message = "Не меньше 1 знаков")
    private String password;


    @Column(name = "email", unique = true, nullable = false)
    @Email
    private String email;

    @ManyToMany(fetch = FetchType.EAGER)
    @Cascade(CascadeType.REMOVE)
    private List<Role> roles;

    @Column(name = "created_at", nullable = false)
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @UpdateTimestamp
    private LocalDateTime  updatedAt;

    @Column(name = "is_remind_enabled", nullable = false)
    private Boolean isNotificationEnabled = true;

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

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof User)) return false;
        User that = (User) o;
        return username.equals(that.username) &&
                email.equals(that.email) &&
                password.equals(that.password);
    }

    @Override
    public int hashCode() {
        return Objects.hash(username, email, password);
    }
}
