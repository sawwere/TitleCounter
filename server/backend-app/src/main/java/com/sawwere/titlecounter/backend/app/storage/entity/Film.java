package com.sawwere.titlecounter.backend.app.storage.entity;

import jakarta.persistence.AttributeOverride;
import jakarta.persistence.AttributeOverrides;
import jakarta.persistence.Column;
import jakarta.persistence.Embedded;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;



@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "films")
public class Film {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name = "title", nullable = false)
    private String title;

    @Column(name = "ru_title")
    private String ruTitle;

    @Column(name = "en_title")
    private String enTitle;

    @Embedded
    @AttributeOverrides({
            @AttributeOverride(name = "imdbId", column = @Column(name = "imdb_id", unique = true)),
            @AttributeOverride(name = "kpId", column = @Column(name = "kp_id", unique = true)),
            @AttributeOverride(name = "tmdbId", column = @Column(name = "tmdb_id", unique = true))
    })
    private FilmExternalId externalId;

    @Column(name = "description", length = 2048)
    private String description;

    @Column(name = "time")
    private Integer time;

    @Column(name = "year_release")
    private Integer yearRelease;

    @Column(name = "date_release")
    private LocalDate dateRelease;

    @Column(name = "global_score")
    private Float globalScore;

    @OneToMany(orphanRemoval = true, mappedBy = "film")
    private List<FilmEntry> filmEntries;

    @Column(name = "created_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @CreationTimestamp
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    @ColumnDefault("'2024-08-04 10:23:54'::timestamp without time zone")
    @UpdateTimestamp
    private LocalDateTime  updatedAt;
}
