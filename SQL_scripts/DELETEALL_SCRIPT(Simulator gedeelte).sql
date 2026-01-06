-- 1. Verwijder de data uit de kind-tabellen
-- De volgorde is belangrijk vanwege de Foreign Key constraints!
DELETE FROM SimulatieKlant;
DELETE FROM GemeenteInstellingen;
DELETE FROM SimulatieInstellingen;

-- 2. Verwijder de data uit de hoofd-tabel
DELETE FROM SimulatieInfo;

-- 3. Reset de ID-tellers (IDENTITY) naar 0
-- Zodat de volgende insert weer bij ID 1 begint
DBCC CHECKIDENT ('SimulatieKlant', RESEED, 0);
DBCC CHECKIDENT ('GemeenteInstellingen', RESEED, 0);
DBCC CHECKIDENT ('SimulatieInstellingen', RESEED, 0);
DBCC CHECKIDENT ('SimulatieInfo', RESEED, 0);