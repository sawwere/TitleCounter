package com.sawwere.titlecounter.backend.app.service;

import com.sawwere.titlecounter.backend.app.dto.film.FilmCreationDto;
import com.sawwere.titlecounter.backend.app.dto.SearchServiceResponseDto;
import com.sawwere.titlecounter.backend.app.dto.game.GameCreationDto;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;


@FeignClient("external-content-search-service")
public interface ExternalContentSearchService {
    @GetMapping("/find/games")
    SearchServiceResponseDto<GameCreationDto> findGames(@RequestParam(value = "title") String title,
                                                        @RequestParam(value = "id") String id);

    @GetMapping("/find/films")
    SearchServiceResponseDto<FilmCreationDto> findFilms(@RequestParam(value = "title") String title,
                                                        @RequestParam(value = "page") String page);

    @GetMapping(value = "/find/image", consumes = {MediaType.IMAGE_PNG_VALUE, MediaType.IMAGE_JPEG_VALUE})
    byte[] findImage(@RequestParam(value = "image_url") String link_url);

}
