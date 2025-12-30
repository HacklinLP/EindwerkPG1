USE KlantenSimulator;

SELECT 
    -- Land Table
    l.id AS LandID,
    l.naam AS LandNaam,

    -- Versie Table
    v.id AS VersieID,
    v.versie AS VersieLabel,

    -- Gemeente Table
    g.id AS GemeenteID,
    g.naam AS GemeenteNaam,

    -- Straat Table
    s.id AS StraatID,
    s.naam AS StraatNaam,

    -- Voornaam Table
    vn.id AS VoornaamID,
    vn.naam AS Voornaam,
    vn.gender AS Gender,
    vn.frequency AS VoornaamFreq,

    -- Achternaam Table
    an.id AS AchternaamID,
    an.naam AS Achternaam,
    an.frequency AS AchternaamFreq

FROM Land l
JOIN Versie v ON l.id = v.landenid
LEFT JOIN Gemeente g ON v.id = g.versieid
LEFT JOIN Straat s ON g.id = s.gemeenteid
LEFT JOIN Voornaam vn ON v.id = vn.versieid
LEFT JOIN Achternaam an ON v.id = an.versieid
ORDER BY LandNaam, VersieLabel, GemeenteNaam;