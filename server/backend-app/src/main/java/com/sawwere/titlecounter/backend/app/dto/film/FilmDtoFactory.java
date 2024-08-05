package com.sawwere.titlecounter.backend.app.dto.film;

import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import org.springframework.stereotype.Component;

@Component
public class FilmDtoFactory {
    public FilmDto entityToDto(Film film) {
        return  FilmDto.builder()
                .id(film.getId())
                .title(film.getTitle())
                .rusTitle(film.getRusTitle())
                .linkUrl(film.getLinkUrl())
                .time(film.getTime())
                .dateRelease(film.getDateRelease())
                .globalScore(film.getGlobalScore())
                .build();
    }

    public Film dtoToEntity(FilmDto filmDto) {
        return Film.builder()
                .id(filmDto.getId())
                .title(filmDto.getTitle())
                .rusTitle(filmDto.getRusTitle())
                .linkUrl(filmDto.getLinkUrl())
                .time(filmDto.getTime())
                .dateRelease(filmDto.getDateRelease())
                .globalScore(filmDto.getGlobalScore())
                .build();
    }

    public Film creationDtoToEntity(FilmCreationDto filmDto) {
        return Film.builder()
                .title(filmDto.getTitle())
                .rusTitle(filmDto.getRusTitle())
                .linkUrl(filmDto.getLinkUrl())
                .time(filmDto.getTime())
                .dateRelease(filmDto.getDateRelease())
                .globalScore(filmDto.getGlobalScore())
                .build();
    }
}
