package com.sawwere.titlecounter.backend.app.storage.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import java.time.LocalDate;
import java.time.LocalDateTime;

@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "film_entries")
public class FilmEntry {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Size(min = 1, max = 64)
    @Column(name = "custom_title", nullable = false)
    private String customTitle;

    @Size(max=512)
    private String note;

    @Min(1)
    @Max(10)
    private Long score;

    @NotBlank
    @Column(nullable = false)
    private String status;

    @Column(name = "date_completed")
    private LocalDate dateCompleted;

    @ManyToOne(fetch = FetchType.EAGER)
    private User user;

    @ManyToOne(fetch = FetchType.EAGER)
    private Film film;

    @Column(name = "created_at", nullable = false)
    @ColumnDefault("2024-08-04 10:23:54")
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @ColumnDefault("2024-08-04 10:23:54")
    @UpdateTimestamp
    private LocalDateTime  updatedAt;
}
