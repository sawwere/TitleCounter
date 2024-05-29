package com.TitleCounter.DataAccess.storage.repository.specification;

import com.TitleCounter.DataAccess.storage.entity.Game;
import org.springframework.data.jpa.domain.Specification;

public class GameSpecification {
    public static Specification<Game> titleContains(String text) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.like(root.get("title"), "%" + text + "%");
    }
}
