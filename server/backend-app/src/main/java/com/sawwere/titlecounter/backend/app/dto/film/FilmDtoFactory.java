package com.sawwere.titlecounter.backend.app.dto.film;

import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import com.sawwere.titlecounter.common.dto.film.FilmDto;
import org.springframework.stereotype.Component;

@Component
public class FilmDtoFactory {
    public FilmDto entityToDto(Film film) {
        return  FilmDto.builder()
                .id(film.getId())
                .title(film.getTitle())
                .alternativeTitle(film.getAlternativeTitle())
                .imdbIdd(film.getImdbIdd())
                .kpId(film.getKpId())
                .time(film.getTime())
                .dateRelease(film.getDateRelease())
                .globalScore(film.getGlobalScore())
                .build();
    }

    public Film dtoToEntity(FilmDto filmDto) {
        return Film.builder()
                .id(filmDto.getId())
                .title(filmDto.getTitle())
                .alternativeTitle(filmDto.getAlternativeTitle())
                .imdbIdd(filmDto.getImdbIdd())
                .kpId(filmDto.getKpId())
                .time(filmDto.getTime())
                .dateRelease(filmDto.getDateRelease())
                .globalScore(filmDto.getGlobalScore())
                .build();
    }

    public Film creationDtoToEntity(FilmCreationDto filmDto) {
        return Film.builder()
                .title(filmDto.getTitle())
                .alternativeTitle(filmDto.getAlternativeTitle())
                .imdbIdd(filmDto.getImdbIdd())
                .kpId(filmDto.getKpId())
                .time(filmDto.getTime())
                .dateRelease(filmDto.getDateRelease())
                .globalScore(filmDto.getGlobalScore())
                .build();
    }
}
