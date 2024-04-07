package com.TitleCounter.DataAccess.storage.entity;

import jakarta.persistence.*;
import lombok.*;

import java.time.LocalDate;

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

    @Column(name = "title")
    private String title;

    @Column(name = "image_url")
    private String imageUrl;

    @Column(name = "link_url")
    private String linkUrl;

    @Column(name = "time")
    private long time;

    @Column(name = "date_release")
    private LocalDate dateRelease;

    @Column(name = "global_score")
    private Float globalScore;

//    @OneToMany(orphanRemoval = true)
//    @JoinColumn(name = "game_entry_id", referencedColumnName = "id")
//    private List<GameEntry> gameEntry;
}
