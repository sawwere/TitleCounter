package com.sawwere.backend.app.storage.repository.specification;

import com.sawwere.backend.app.storage.entity.Film;
import org.springframework.data.jpa.domain.Specification;

public class FilmSpecification {
    public static Specification<Film> titleContains(String text) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.like(root.get("title"), "%" + text + "%");
    }
}
