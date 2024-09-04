package com.sawwere.titlecounter.backend.app.dto;

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
