package com.TitleCounter.AuthorizationServer.storage.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.*;

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

    @Size(max=255)
    private String note;

    @Min(0)
    @Max(10)
    private Long score;

    @NotBlank
    private String status;

    @Column(name = "date_completed")
    private Long dateCompleted;

    @ManyToOne(fetch = FetchType.EAGER)
    private User user;

    @ManyToOne(fetch = FetchType.EAGER)
    private Film film;
}
