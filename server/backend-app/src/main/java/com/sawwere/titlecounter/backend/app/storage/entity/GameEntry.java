package com.sawwere.titlecounter.backend.app.storage.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.*;
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
@Table(name = "game_entries")
public class GameEntry {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Size(min = 1, max = 64)
    @Column(name = "custom_title")
    private String customTitle;

    @Size(max=512)
    private String note;

    @Min(1)
    @Max(10)
    private Integer score;

    @NotBlank
    @Column(nullable = false)
    private String status;

    @Column(name = "date_completed")
    private LocalDate dateCompleted;

    @Min(0)
    private Long time;

    @Size(max = 64)
    private String platform;

    @NotNull
    @ManyToOne(fetch = FetchType.EAGER)
    private User user;

    @NotNull
    @ManyToOne(fetch = FetchType.EAGER)
    private Game game;

    @Column(name = "created_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @UpdateTimestamp
    private LocalDateTime  updatedAt;
}
