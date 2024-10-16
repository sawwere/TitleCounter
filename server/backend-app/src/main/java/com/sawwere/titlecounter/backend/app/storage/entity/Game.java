package com.sawwere.titlecounter.backend.app.storage.entity;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.persistence.*;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
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

    @Column(name = "time")
    private long time;

    @Column(name = "date_release")
    private LocalDate dateRelease;

    @Column(name = "global_score")
    private Float globalScore;

    @Column(name = "game_type")
    private String gameType;

    @Column(name  = "developer")
    private String developer;

    @Column(name = "description", length = 2048)
    private String description;

    @Embedded
    @AttributeOverrides({
            @AttributeOverride(name = "hltbId", column = @Column(name = "hltb_id", unique = true)),
            @AttributeOverride(name = "steamId", column = @Column(name = "steam_id", unique = true))
    })
    private GameExternalId externalId;

    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(
            name = "games_game_platforms",
            joinColumns = @JoinColumn(name = "game_id"),
            inverseJoinColumns = @JoinColumn(name = "game_platform_id")
    )
    private List<GamePlatform> platforms;

    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(
            name = "games_game_genres",
            joinColumns = @JoinColumn(name = "game_id"),
            inverseJoinColumns = @JoinColumn(name = "game_genre_id")
    )
    private List<GameGenre> genres;

    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(
            name = "games_game_developers",
            joinColumns = @JoinColumn(name = "game_id"),
            inverseJoinColumns = @JoinColumn(name = "game_developer_id")
    )
    private List<GameDeveloper> developers;

    @OneToMany(orphanRemoval = true, mappedBy = "game")
    private List<GameEntry> gameEntries;

    @Column(name = "created_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @UpdateTimestamp
    private LocalDateTime  updatedAt;
}
