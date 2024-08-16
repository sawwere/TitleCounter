package com.sawwere.titlecounter.backend.app.dto.film;

import lombok.*;

import java.util.List;

@Getter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class SearchServiceResponseDto<T> {
    private Integer total;
    private List<T> contents;
}
