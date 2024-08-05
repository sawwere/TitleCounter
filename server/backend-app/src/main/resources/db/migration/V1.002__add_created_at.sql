ALTER TABLE ONLY films
    ADD created_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54',
    ADD updated_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54';
ALTER TABLE ONLY games
    ADD created_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54',
    ADD updated_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54';
ALTER TABLE ONLY users
    ADD created_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54',
    ADD updated_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54';
ALTER TABLE ONLY film_entries
    ADD created_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54',
    ADD updated_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54';
ALTER TABLE ONLY game_entries
    ADD created_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54',
    ADD updated_at timestamp NOT NULL DEFAULT '2024-08-04 10:23:54';