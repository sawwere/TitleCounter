package com.sawwere.titlecounter.backend.app.storage.entity;

import jakarta.persistence.Embeddable;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Embeddable
public class FilmExternalId {
    private String imdbId;

    private String kpId;

    private String tmdbId;
}
