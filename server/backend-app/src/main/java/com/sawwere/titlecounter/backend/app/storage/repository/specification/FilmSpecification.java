package com.sawwere.titlecounter.backend.app.storage.repository.specification;

import com.sawwere.titlecounter.backend.app.storage.entity.Film;
import org.springframework.data.jpa.domain.Specification;

public final class FilmSpecification {
    private FilmSpecification() {}

    public static Specification<Film> titleContains(String text) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.like(root.get("title"), "%" + text + "%");
    }
}
