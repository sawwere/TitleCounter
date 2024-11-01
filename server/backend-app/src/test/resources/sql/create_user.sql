INSERT INTO users (id, email, password, username) VALUES
    (101, 'test1@test.test', '$2a$10$P4cQkHsnl3T56a.GQgXP6Oq/0MvDEh.HTXMhOSatfJHD3qR6NVHPa', 'test_user0');

INSERT INTO users_roles(role_id, user_id) VALUES
    (2, 101);