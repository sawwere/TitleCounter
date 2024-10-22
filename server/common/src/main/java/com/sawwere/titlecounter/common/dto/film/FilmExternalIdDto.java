package com.sawwere.titlecounter.common.dto.film;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class FilmExternalIdDto {
    @JsonProperty("imdb_id")
    private String imdbId;
    @JsonProperty("kp_id")
    private String kpId;
    @JsonProperty("tmdb_id")
    private String tmdbId;
}
