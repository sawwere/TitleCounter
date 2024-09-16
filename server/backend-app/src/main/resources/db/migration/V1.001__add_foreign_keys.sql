ALTER TABLE users_roles DROP CONSTRAINT IF EXISTS user_roles_user_id_fk;
ALTER TABLE ONLY users_roles ADD CONSTRAINT user_roles_user_id_fk FOREIGN KEY (user_id) REFERENCES users(id);

ALTER TABLE users_roles DROP CONSTRAINT IF EXISTS user_roles_role_id_fk;
ALTER TABLE ONLY users_roles ADD CONSTRAINT user_roles_role_id_fk FOREIGN KEY (role_id) REFERENCES roles(id);

ALTER TABLE game_entries DROP CONSTRAINT IF EXISTS game_entries_game_fk;
ALTER TABLE ONLY game_entries ADD CONSTRAINT game_entries_game_fk FOREIGN KEY (game_id) REFERENCES games(id);
ALTER TABLE game_entries DROP CONSTRAINT IF EXISTS game_entries_user_fk;
ALTER TABLE ONLY game_entries ADD CONSTRAINT game_entries_user_fk FOREIGN KEY (user_id) REFERENCES users(id);

ALTER TABLE film_entries DROP CONSTRAINT IF EXISTS film_entries_user_fk;
ALTER TABLE ONLY film_entries ADD CONSTRAINT film_entries_user_fk FOREIGN KEY (user_id) REFERENCES users(id);
ALTER TABLE film_entries DROP CONSTRAINT IF EXISTS film_entries_film_fk;
ALTER TABLE ONLY film_entries ADD CONSTRAINT film_entries_film_fk FOREIGN KEY (film_id) REFERENCES films(id);