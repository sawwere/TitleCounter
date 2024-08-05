package com.sawwere.backend.app.storage.repository;

import com.sawwere.backend.app.storage.entity.Film;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.stream.Stream;

public interface FilmRepository extends JpaRepository<Film, Long>, JpaSpecificationExecutor<Film> {
    Stream<Film> streamAllBy();
}
