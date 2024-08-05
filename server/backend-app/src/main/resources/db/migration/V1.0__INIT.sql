CREATE TABLE roles (
    id bigint NOT NULL,
    name character varying(255)
);

ALTER TABLE ONLY roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);

--USERS
CREATE TABLE users (
    id bigint NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    username character varying(255) NOT NULL
);

CREATE SEQUENCE users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
ALTER SEQUENCE users_id_seq OWNED BY users.id;
ALTER TABLE ONLY users ALTER COLUMN id SET DEFAULT nextval('users_id_seq'::regclass);

ALTER TABLE ONLY users
    ADD CONSTRAINT unique_email_idx UNIQUE (email);
ALTER TABLE ONLY users
    ADD CONSTRAINT unique_username_idx UNIQUE (username);
ALTER TABLE ONLY users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);

--USER_ROLES
CREATE TABLE users_roles (
    roles_id bigint NOT NULL,
    user_id bigint NOT NULL
);

ALTER TABLE users_roles OWNER TO postgres;

ALTER TABLE ONLY users_roles
    ADD CONSTRAINT user_roles_user_id_fk FOREIGN KEY (user_id) REFERENCES users(id);
ALTER TABLE ONLY users_roles
    ADD CONSTRAINT user_roles_role_id_fk FOREIGN KEY (roles_id) REFERENCES roles(id);

--GAMES
CREATE TABLE games (
    id bigint NOT NULL,
    date_release date,
    global_score real,
    link_url character varying(255),
    "time" bigint NOT NULL,
    title character varying(255) NOT NULL
);

CREATE SEQUENCE games_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE games_id_seq OWNED BY games.id;

ALTER TABLE ONLY games ALTER COLUMN id SET DEFAULT nextval('games_id_seq'::regclass);

ALTER TABLE ONLY games
    ADD CONSTRAINT games_pkey PRIMARY KEY (id);

--GAME_ENTRIES
CREATE TABLE game_entries (
     id bigint NOT NULL,
     custom_title character varying(64),
     date_completed date,
     note character varying(255),
     platform character varying(255),
     score bigint,
     status character varying(255),
     "time" bigint,
     game_id bigint NOT NULL,
     user_id bigint NOT NULL,
     CONSTRAINT game_entries_score_check CHECK (((score >= 0) AND (score <= 10))),
     CONSTRAINT game_entries_time_check CHECK (("time" >= 0))
);

CREATE SEQUENCE game_entries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE game_entries_id_seq OWNED BY game_entries.id;

ALTER TABLE ONLY game_entries ALTER COLUMN id SET DEFAULT nextval('game_entries_id_seq'::regclass);

ALTER TABLE ONLY game_entries
    ADD CONSTRAINT game_entries_pkey PRIMARY KEY (id);

ALTER TABLE ONLY game_entries
    ADD CONSTRAINT game_entries_game_fk FOREIGN KEY (game_id) REFERENCES games(id);
ALTER TABLE ONLY game_entries
    ADD CONSTRAINT game_entries_user_fk FOREIGN KEY (user_id) REFERENCES users(id);

--FILMS
CREATE TABLE films (
    id bigint NOT NULL,
    date_release date,
    global_score real,
    link_url character varying(255),
    rus_title character varying(255),
    "time" bigint,
    title character varying(255) NOT NULL
);

CREATE SEQUENCE films_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE films_id_seq OWNED BY films.id;

ALTER TABLE ONLY films ALTER COLUMN id SET DEFAULT nextval('films_id_seq'::regclass);

ALTER TABLE ONLY films
    ADD CONSTRAINT films_pkey PRIMARY KEY (id);

--FILM_ENTRIES
CREATE TABLE film_entries (
     id bigint NOT NULL,
     custom_title character varying(64),
     date_completed date,
     note character varying(255),
     score bigint,
     status character varying(255),
     film_id bigint,
     user_id bigint,
     CONSTRAINT film_entries_score_check CHECK (((score >= 0) AND (score <= 10)))
);

CREATE SEQUENCE film_entries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE film_entries_id_seq OWNED BY film_entries.id;

ALTER TABLE ONLY film_entries ALTER COLUMN id SET DEFAULT nextval('film_entries_id_seq'::regclass);

ALTER TABLE ONLY film_entries
    ADD CONSTRAINT film_entries_pkey PRIMARY KEY (id);
ALTER TABLE ONLY film_entries
    ADD CONSTRAINT film_entries_user_fk FOREIGN KEY (user_id) REFERENCES users(id);

ALTER TABLE ONLY film_entries
    ADD CONSTRAINT film_entries_film_fk FOREIGN KEY (film_id) REFERENCES films(id);