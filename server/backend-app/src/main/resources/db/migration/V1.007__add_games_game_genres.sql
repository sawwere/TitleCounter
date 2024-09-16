CREATE TABLE IF NOT EXISTS games_game_genres (
    game_id bigint NOT NULL,
    game_genre_id bigint NOT NULL
);

ALTER TABLE games_game_genres DROP CONSTRAINT IF EXISTS games_game_genres_game_id_fk;
ALTER TABLE ONLY games_game_genres
    ADD CONSTRAINT games_game_genres_game_id_fk FOREIGN KEY (game_id) REFERENCES games(id);
ALTER TABLE games_game_genres DROP CONSTRAINT IF EXISTS games_game_genres_genre_id_fk;
ALTER TABLE ONLY games_game_genres
    ADD CONSTRAINT games_game_genres_genre_id_fk FOREIGN KEY (game_genre_id) REFERENCES game_genres(id);