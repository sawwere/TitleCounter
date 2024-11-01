INSERT INTO game_entries VALUES
    (1, 'F.E.A.R.', '2022-09-01', '', 'PC', 9, 'completed', 491, 1, 1, '2024-08-04 10:23:54', '2024-09-06 22:17:45.751168'),
    (2, 'Warcraft III: Reign of Chaos', '2023-12-27', '', 'PC', 8, 'completed', 1260, 2, 1, '2024-08-04 10:23:54', '2024-09-06 22:17:48.950821'),
    (3, 'Wolfenstein II: The New Colossus', NULL, '', 'PC', 2, 'retired', 646, 3, 1, '2024-08-04 10:23:54', '2024-08-04 10:23:54'),
    (4, 'XCOM: Enemy Unknown', '2024-04-07', '', 'PC', 8, 'completed', 1604, 4, 1, '2024-08-04 10:23:54', '2024-09-06 21:36:42.751077'),
    (5, 'Yakuza 0', NULL, '', 'PC', 3, 'retired', 1883, 5, 1, '2024-08-04 10:23:54', '2024-08-04 10:23:54');

SELECT pg_catalog.setval('game_entries_id_seq', 5, true);