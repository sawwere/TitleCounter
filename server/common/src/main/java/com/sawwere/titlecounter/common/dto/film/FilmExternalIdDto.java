package com.sawwere.titlecounter.common.dto.film;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class FilmExternalIdDto {
    @JsonProperty("imdb_id")
    private String imdbIdd;
    @JsonProperty("kp_id")
    private String kpId;
    @JsonProperty("tmdb_id")
    private String tmdbId;
}
