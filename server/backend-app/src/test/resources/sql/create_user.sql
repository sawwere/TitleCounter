INSERT INTO roles(id, name) VALUES
    (1,	'ADMIN'),
    (2,	'USER')
ON CONFLICT DO NOTHING;

INSERT INTO users (id, email, password, username) VALUES
    (1, 'zxc@gm.kukuasd', '$2a$10$P4cQkHsnl3T56a.GQgXP6Oq/0MvDEh.HTXMhOSatfJHD3qR6NVHPa', 'admin'),
    (2, 'a@a.a', '$2a$10$cQ6wPjpl3fARjwWI1Zq7LebXe4ErtuX0YaOd/Aw5EbeE42BT/DCWq', 'a')
ON CONFLICT DO NOTHING;

INSERT INTO users_roles(user_id, role_id) VALUES
    (1, 1),
    (1, 2),
    (2, 2)
ON CONFLICT DO NOTHING;

INSERT INTO users (id, email, password, username) VALUES
    (101, 'test1@test.test', '$2a$10$P4cQkHsnl3T56a.GQgXP6Oq/0MvDEh.HTXMhOSatfJHD3qR6NVHPa', 'test_user0')
ON CONFLICT DO NOTHING;

INSERT INTO users_roles(role_id, user_id) VALUES
    (2, 101)
ON CONFLICT DO NOTHING;