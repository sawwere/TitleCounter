package com.sawwere.titlecounter.backend.app.dto.film;

import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import com.sawwere.titlecounter.backend.app.storage.entity.FilmEntry;
import com.sawwere.titlecounter.backend.app.storage.entity.User;
import com.sawwere.titlecounter.common.dto.film.FilmEntryRequestDto;
import com.sawwere.titlecounter.common.dto.film.FilmEntryResponseDto;
import java.time.LocalDate;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Component;


@RequiredArgsConstructor
@Component
public class FilmEntryDtoFactory {
    private final FilmDtoFactory filmDtoFactory;

    public FilmEntryResponseDto entityToDto(FilmEntry filmEntry) {
        return  FilmEntryResponseDto.builder()
                .id(filmEntry.getId())
                .customTitle(filmEntry.getCustomTitle())
                .note(filmEntry.getNote())
                .score(filmEntry.getScore())
                .status(filmEntry.getStatus())
                .dateCompleted(filmEntry.getDateCompleted())
                .film(filmDtoFactory.entityToDto(filmEntry.getFilm()))
                .userId(filmEntry.getUser().getId())
                .build();
    }

    public FilmEntry dtoToEntity(FilmEntryRequestDto filmEntryRequestDto) {
        return FilmEntry.builder()
                .id(filmEntryRequestDto.getId())
                .customTitle(filmEntryRequestDto.getCustomTitle())
                .note(filmEntryRequestDto.getNote())
                .score(filmEntryRequestDto.getScore())
                .status(filmEntryRequestDto.getStatus())
                .dateCompleted(filmEntryRequestDto.getDateCompleted())
                .film(Film.builder().id(filmEntryRequestDto.getFilmId()).build())
                .user(User.builder().id(filmEntryRequestDto.getUserId()).build())
                .build();
    }

    public FilmEntryRequestDto makeDefault(Long filmId, Long userId) {
        return  FilmEntryRequestDto.builder()
                .score(0)
                .status("completed")
                .dateCompleted(LocalDate.now())
                .filmId(filmId)
                .userId(userId)
                .build();
    }
}
