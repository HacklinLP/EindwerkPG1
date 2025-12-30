-- 1. Delete all data (be careful, this empties everything!)
-- You must delete in this order because of Foreign Keys
DELETE FROM Straat;
DELETE FROM Gemeente;
DELETE FROM Voornaam;
DELETE FROM Versie;
DELETE FROM Land;


-- 2. Reset the identity counters back to 0
DBCC CHECKIDENT ('Land', RESEED, 0);
DBCC CHECKIDENT ('Versie', RESEED, 0);
DBCC CHECKIDENT ('Voornaam', RESEED, 0);
DBCC CHECKIDENT ('Gemeente', RESEED, 0);
DBCC CHECKIDENT ('Straat', RESEED, 0);
