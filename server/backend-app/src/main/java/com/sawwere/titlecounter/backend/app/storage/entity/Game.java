package com.sawwere.titlecounter.backend.app.storage.entity;

import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;

@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name="games")
public class Game {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name = "title", nullable = false)
    private String title;

    @Column(name = "hltb_id", unique = true)
    private String hltbId;

    @Column(name = "time")
    private long time;

    @Column(name = "date_release")
    private LocalDate dateRelease;

    @Column(name = "global_score")
    private Float globalScore;

    @OneToMany(orphanRemoval = true, mappedBy = "game")
    private List<GameEntry> gameEntries;

    @Column(name = "created_at", nullable = false)
    @ColumnDefault("2024-08-04 10:23:54")
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @ColumnDefault("2024-08-04 10:23:54")
    @UpdateTimestamp
    private LocalDateTime  updatedAt;
}
