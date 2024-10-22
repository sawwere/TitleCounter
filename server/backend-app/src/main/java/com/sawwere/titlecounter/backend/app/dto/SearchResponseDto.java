package com.sawwere.titlecounter.backend.app.dto;

import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;


@Getter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class SearchResponseDto<T> {
    private Integer total;
    private List<T> contents;
}
