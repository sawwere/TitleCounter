CREATE TABLE IF NOT EXISTS roles (
    id bigint NOT NULL,
    name character varying(255)
);

ALTER TABLE roles DROP CONSTRAINT IF EXISTS roles_pkey CASCADE;
ALTER TABLE roles ADD CONSTRAINT roles_pkey PRIMARY KEY (id);

--USERS
CREATE TABLE IF NOT EXISTS users (
    id bigint NOT NULL,
    email character varying(255) NOT NULL UNIQUE,
    password character varying(255) NOT NULL,
    username character varying(255) NOT NULL UNIQUE,
    is_remind_enabled boolean DEFAULT true NOT NULL,
    is_enabled boolean DEFAULT false NOT NULL,
    is_locked boolean DEFAULT false NOT NULL,
    created_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    updated_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL
);

CREATE SEQUENCE IF NOT EXISTS users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
ALTER SEQUENCE users_id_seq OWNED BY users.id;
ALTER TABLE ONLY users ALTER COLUMN id SET DEFAULT nextval('users_id_seq'::regclass);

ALTER TABLE ONLY users DROP CONSTRAINT IF EXISTS unique_email_idx;
ALTER TABLE ONLY users ADD CONSTRAINT unique_email_idx UNIQUE (email);

ALTER TABLE ONLY users DROP CONSTRAINT IF EXISTS unique_username_idx;
ALTER TABLE ONLY users ADD CONSTRAINT unique_username_idx UNIQUE (username);

ALTER TABLE users DROP CONSTRAINT IF EXISTS users_pkey CASCADE;
ALTER TABLE users ADD CONSTRAINT users_pkey PRIMARY KEY (id);

--USER_ROLES
CREATE TABLE IF NOT EXISTS users_roles (
    role_id bigint NOT NULL,
    user_id bigint NOT NULL
);

--GAMES
CREATE TABLE IF NOT EXISTS games (
    id bigint NOT NULL,
    date_release date,
    global_score real,
    hltb_id character varying(255),
    "time" bigint NOT NULL,
    title character varying(255) NOT NULL,
    game_type character varying(255),
    steam_id character varying(255),
    description character varying(2048),
    developer character varying(255),
    created_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    updated_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL
);

CREATE SEQUENCE IF NOT EXISTS games_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE games_id_seq OWNED BY games.id;

ALTER TABLE ONLY games ALTER COLUMN id SET DEFAULT nextval('games_id_seq'::regclass);

ALTER TABLE games DROP CONSTRAINT IF EXISTS games_pkey CASCADE;
ALTER TABLE ONLY games ADD CONSTRAINT games_pkey PRIMARY KEY (id);

ALTER TABLE ONLY games DROP CONSTRAINT IF EXISTS unique_hltb_id_idx;
ALTER TABLE ONLY games ADD CONSTRAINT unique_hltb_id_idx UNIQUE (hltb_id);

--GAME_ENTRIES
CREATE TABLE IF NOT EXISTS game_entries (
    id bigint NOT NULL,
    custom_title character varying(64),
    date_completed date,
    note character varying(512),
    platform character varying(64),
    score integer,
    status character varying(255),
    "time" bigint,
    game_id bigint NOT NULL,
    user_id bigint NOT NULL,
    created_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    updated_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    CONSTRAINT game_entries_score_check CHECK (((score >= 1) AND (score <= 10))),
    CONSTRAINT game_entries_time_check CHECK (("time" >= 0))
);

CREATE SEQUENCE IF NOT EXISTS game_entries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE game_entries_id_seq OWNED BY game_entries.id;

ALTER TABLE ONLY game_entries ALTER COLUMN id SET DEFAULT nextval('game_entries_id_seq'::regclass);

ALTER TABLE game_entries DROP CONSTRAINT IF EXISTS game_entries_pkey CASCADE;
ALTER TABLE ONLY game_entries ADD CONSTRAINT game_entries_pkey PRIMARY KEY (id);

--FILMS
CREATE TABLE IF NOT EXISTS films (
    id bigint NOT NULL,
    title character varying(255) NOT NULL,
    ru_title character varying(255),
    en_title character varying(255),
    imdb_id character varying(255),
    kp_id character varying(255),
    tmdb_id character varying(255),
    "time" integer,
    date_release date,
    year_release integer,
    global_score real,
    description character varying(2048) NOT NULL,
    created_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    updated_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL
);

CREATE SEQUENCE IF NOT EXISTS films_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE films_id_seq OWNED BY films.id;

ALTER TABLE ONLY films ALTER COLUMN id SET DEFAULT nextval('films_id_seq'::regclass);

ALTER TABLE films DROP CONSTRAINT IF EXISTS films_pkey CASCADE;
ALTER TABLE ONLY films ADD CONSTRAINT films_pkey PRIMARY KEY (id);

ALTER TABLE ONLY films DROP CONSTRAINT IF EXISTS unique_imdb_id_idx;
ALTER TABLE ONLY films ADD CONSTRAINT unique_imdb_id_idx UNIQUE (imdb_id);

ALTER TABLE ONLY films DROP CONSTRAINT IF EXISTS unique_kp_id_idx;
ALTER TABLE ONLY films ADD CONSTRAINT unique_kp_id_idx UNIQUE (kp_id);

--FILM_ENTRIES
CREATE TABLE IF NOT EXISTS film_entries (
    id bigint NOT NULL,
    custom_title character varying(64),
    date_completed date,
    note character varying(512),
    score integer,
    status character varying(255) NOT NULL,
    film_id bigint NOT NULL,
    user_id bigint NOT NULL,
    created_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    updated_at timestamp without time zone DEFAULT '2024-08-04 10:23:54'::timestamp without time zone NOT NULL,
    CONSTRAINT film_entries_score_check CHECK (((score >= 1) AND (score <= 10)))
);

CREATE SEQUENCE IF NOT EXISTS film_entries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE film_entries_id_seq OWNED BY film_entries.id;

ALTER TABLE ONLY film_entries ALTER COLUMN id SET DEFAULT nextval('film_entries_id_seq'::regclass);

ALTER TABLE film_entries DROP CONSTRAINT IF EXISTS film_entries_pkey CASCADE;
ALTER TABLE ONLY film_entries ADD CONSTRAINT film_entries_pkey PRIMARY KEY (id);