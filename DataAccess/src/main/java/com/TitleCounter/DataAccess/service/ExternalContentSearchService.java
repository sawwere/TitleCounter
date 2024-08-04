package com.TitleCounter.DataAccess.service;

import com.TitleCounter.DataAccess.dto.film.FilmCreationDto;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;


@FeignClient("external-content-search-service")
public interface ExternalContentSearchService {
    @GetMapping("/find/films")
    List<FilmCreationDto> findFilms(@RequestParam(value = "title") String title, @RequestParam(value = "page") int page);

    @GetMapping(value = "/find/image", consumes = {MediaType.IMAGE_PNG_VALUE, MediaType.IMAGE_JPEG_VALUE})
    byte[] findImage(@RequestParam(value = "image_url") String link_url);
}
