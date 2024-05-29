package com.TitleCounter.DataAccess.storage.repository.specification;

import com.TitleCounter.DataAccess.storage.entity.Film;
import org.springframework.data.jpa.domain.Specification;

public class FilmSpecification {
    public static Specification<Film> titleContains(String text) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.like(root.get("title"), "%" + text + "%");
    }
}
