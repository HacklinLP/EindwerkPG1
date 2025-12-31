-- 1. Verwijder eerst de 'kind-tabellen' (deze verwijzen naar Versie)
DELETE FROM Voornaam;
DELETE FROM Achternaam;
DELETE FROM Straat;

-- 2. Verwijder daarna de gemeentes (deze verwijzen ook naar Versie)
DELETE FROM Gemeente;

-- 3. Nu zijn alle verwijzingen naar 'Versie' weg, dus kunnen we de versies wissen
DELETE FROM Versie;

-- 4. Als laatste kun je de landen wissen
DELETE FROM Land;


-- 2. Reset the identity counters back to 0
DBCC CHECKIDENT ('Voornaam', RESEED, 0);
DBCC CHECKIDENT ('Achternaam', RESEED, 0);
DBCC CHECKIDENT ('Straat', RESEED, 0);
DBCC CHECKIDENT ('Gemeente', RESEED, 0);
DBCC CHECKIDENT ('Versie', RESEED, 0);
DBCC CHECKIDENT ('Land', RESEED, 0);
