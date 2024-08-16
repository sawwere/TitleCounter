ALTER TABLE ONLY films
    ADD CONSTRAINT unique_link_url_films_idx UNIQUE (link_url);
ALTER TABLE ONLY games
    ADD CONSTRAINT unique_link_url_games_idx UNIQUE (link_url);