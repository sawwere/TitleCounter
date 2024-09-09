package com.sawwere.titlecounter.backend.app.dto.film;

import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import com.sawwere.titlecounter.backend.app.storage.entity.FilmExternalId;
import com.sawwere.titlecounter.common.dto.film.FilmDto;
import com.sawwere.titlecounter.common.dto.film.FilmExternalIdDto;
import org.springframework.stereotype.Component;

@Component
public class FilmDtoFactory {
    public FilmDto entityToDto(Film film) {
        return  FilmDto.builder()
                .id(film.getId())
                .title(film.getTitle())
                .ruTitle(film.getRuTitle())
                .enTitle(film.getEnTitle())
                .description(film.getDescription())
                .externalId(map(film.getExternalId()))
                .time(film.getTime())
                .yearRelease(film.getYearRelease())
                .dateRelease(film.getDateRelease())
                .globalScore(film.getGlobalScore())
                .build();
    }

    public Film dtoToEntity(FilmDto filmDto) {
        return Film.builder()
                .id(filmDto.getId())
                .title(filmDto.getTitle())
                .ruTitle(filmDto.getRuTitle())
                .enTitle(filmDto.getEnTitle())
                .description(filmDto.getDescription())
                .externalId(map(filmDto.getExternalId()))
                .time(filmDto.getTime())
                .yearRelease(filmDto.getYearRelease())
                .dateRelease(filmDto.getDateRelease())
                .globalScore(filmDto.getGlobalScore())
                .build();
    }

    public Film creationDtoToEntity(FilmCreationDto filmDto) {
        return Film.builder()
                .title(filmDto.getTitle())
                .ruTitle(filmDto.getRuTitle())
                .enTitle(filmDto.getEnTitle())
                .description(filmDto.getDescription())
                .externalId(map(filmDto.getExternalId()))
                .time(filmDto.getTime())
                .yearRelease(filmDto.getYearRelease())
                .dateRelease(filmDto.getDateRelease())
                .globalScore(filmDto.getGlobalScore())
                .build();
    }


    private FilmExternalIdDto map(FilmExternalId obj) {
        return FilmExternalIdDto.builder()
                .kpId(obj.getKpId())
                .tmdbId(obj.getTmdbId())
                .imdbIdd(obj.getImdbIdd())
                .build();
    }

    private FilmExternalId map(FilmExternalIdDto obj) {
        return FilmExternalId.builder()
                .kpId(obj.getKpId())
                .tmdbId(obj.getTmdbId())
                .imdbIdd(obj.getImdbIdd())
                .build();
    }
}
