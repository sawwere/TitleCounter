package com.TitleCounter.AuthorizationServer.storage.entity;

import jakarta.persistence.*;
import lombok.*;

import java.time.LocalDate;
import java.util.List;

@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name="films")
public class Film {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name = "title")
    private String title;

    @Column(name = "rus_title")
    private String rusTitle;

    @Column(name = "image_url")
    private String imageUrl;

    @Column(name = "link_url")
    private String linkUrl;

    @Column(name = "time")
    private Long time;

    @Column(name = "date_release")
    private LocalDate dateRelease;

    @Column(name = "global_score")
    private Float globalScore;

    @OneToMany(orphanRemoval = true)
    @JoinColumn(name = "game_entry_id", referencedColumnName = "id")
    private List<FilmEntry> filmEntries;
}
