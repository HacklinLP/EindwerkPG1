--USE KlantenSimulator;

--SELECT 
--    l.naam AS LandNaam,
--    v.versie AS VersieLabel,
--    g.naam AS GemeenteNaam,
--    s.id AS StraatID,
--    s.naam AS StraatNaam
--FROM Land l
--JOIN Versie v ON l.id = v.landenid
--JOIN Gemeente g ON v.id = g.versieid
--JOIN Straat s ON g.id = s.gemeenteid
--ORDER BY l.id, GemeenteNaam, StraatNaam;


-- SWITCH


--USE KlantenSimulator;

--SELECT 
--    l.naam AS LandNaam,
--    v.versie AS VersieLabel,
--    vn.id AS VoornaamID,
--    vn.naam AS Voornaam,
--    vn.gender AS Gender,
--    vn.frequency AS Frequentie
--FROM Land l
--JOIN Versie v ON l.id = v.landenid
--JOIN Voornaam vn ON v.id = vn.versieid
--ORDER BY l.id, Voornaam;


-- SWITCH


USE KlantenSimulator;

SELECT 
    l.naam AS LandNaam,
    v.versie AS VersieLabel,
    an.id AS AchternaamID,
    an.naam AS Achternaam,
    an.frequency AS Frequentie
FROM Land l
JOIN Versie v ON l.id = v.landenid
JOIN Achternaam an ON v.id = an.versieid
ORDER BY LandNaam, Achternaam;