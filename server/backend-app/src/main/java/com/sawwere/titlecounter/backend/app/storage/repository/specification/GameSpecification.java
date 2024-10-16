package com.sawwere.titlecounter.backend.app.storage.repository.specification;

import com.sawwere.titlecounter.backend.app.storage.entity.Game;
import java.time.LocalDate;
import java.util.Locale;

import jakarta.persistence.criteria.Expression;
import org.springframework.data.jpa.domain.Specification;

@SuppressWarnings("checkstyle:MultipleStringLiterals")
public final class GameSpecification {
    private GameSpecification() {}

    public static Specification<Game> titleContains(String text) {
        return (root, query, criteriaBuilder) ->
                criteriaBuilder.like(
                        criteriaBuilder.lower(root.get("title")),
                        ("%" + text + "%").toLowerCase(Locale.ROOT)
                );
    }

    public static Specification<Game> scoreGreaterThan(float globalScore) {
        return (root, query, criteriaBuilder) ->
                criteriaBuilder.greaterThan(root.get("globalScore"), globalScore);
    }

    public static Specification<Game> scoreLessThan(float globalScore) {
        return (root, query, criteriaBuilder) ->
                criteriaBuilder.lessThan(root.get("globalScore"), globalScore);
    }

    public static Specification<Game> timeGreaterThan(long time) {
        return (root, query, criteriaBuilder) ->
                criteriaBuilder.greaterThan(root.get("time"), time);
    }

    public static Specification<Game> timeLessThan(float time) {
        return (root, query, criteriaBuilder) ->
                criteriaBuilder.greaterThan(root.get("time"), time);
    }

    public static Specification<Game> releaseFrom(LocalDate date) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.greaterThan(root.get("dateRelease"), date);
    }

    public static Specification<Game> releaseBefore(LocalDate date) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.lessThan(root.get("dateRelease"), date);
    }
}
