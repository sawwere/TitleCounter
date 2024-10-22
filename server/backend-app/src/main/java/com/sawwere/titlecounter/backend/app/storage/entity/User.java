package com.sawwere.titlecounter.backend.app.storage.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Index;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.JoinTable;
import jakarta.persistence.ManyToMany;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.Size;
import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;
import java.util.Objects;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.hibernate.annotations.Cascade;
import org.hibernate.annotations.CascadeType;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

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
    @Size(min = 3, message = "Не меньше 3 знаков")
    private String username;

    @Column(name = "password", nullable = false)
    @Size(min = 10, message = "Не меньше 10 знаков")
    private String password;

    @Column(name = "email", unique = true, nullable = false)
    @Email
    private String email;

    @Column(name = "is_enabled", nullable = false)
    @ColumnDefault("false")
    @Builder.Default
    private Boolean isEnabled = Boolean.FALSE;

    @Column(name = "is_locked", nullable = false)
    @ColumnDefault("false")
    @Builder.Default
    private Boolean isLocked = Boolean.FALSE;

    @Column(name = "is_remind_enabled", nullable = false)
    @ColumnDefault("true")
    @Builder.Default
    private Boolean isRemindEnabled = Boolean.TRUE;

    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(
            name = "users_roles",
            joinColumns = @JoinColumn(name = "user_id"),
            inverseJoinColumns = @JoinColumn(name = "role_id")
    )
    @Cascade(CascadeType.REMOVE)
    private List<Role> roles;

    @OneToMany(orphanRemoval = true)
    @JoinColumn(name = "user_id", referencedColumnName = "id")
    private List<GameEntry> gameEntry;

    @OneToMany(orphanRemoval = true)
    @JoinColumn(name = "user_id", referencedColumnName = "id")
    private List<FilmEntry> filmEntry;

    @Column(name = "created_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @UpdateTimestamp
    private LocalDateTime  updatedAt;

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
        return !isLocked;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    @Override
    public boolean isEnabled() {
        return isEnabled;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (!(o instanceof User)) {
            return false;
        }
        User that = (User) o;
        return username.equals(that.username)
                && email.equals(that.email)
                && password.equals(that.password);
    }

    @Override
    public int hashCode() {
        return Objects.hash(username, email, password);
    }
}
