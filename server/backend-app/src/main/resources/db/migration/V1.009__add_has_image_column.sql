ALTER TABLE games ADD has_image BOOLEAN;
ALTER TABLE games ALTER COLUMN  has_image SET DEFAULT TRUE;
UPDATE
  games
SET
  has_image = 'true'
WHERE
  has_image IS NULL;
ALTER TABLE games ALTER COLUMN  has_image SET NOT NULL;

ALTER TABLE films ADD has_image BOOLEAN;
ALTER TABLE films ALTER COLUMN  has_image SET DEFAULT TRUE;
UPDATE
  films
SET
  has_image = 'true'
WHERE
  has_image IS NULL;
ALTER TABLE films ALTER COLUMN  has_image SET NOT NULL;
