USE KlantenSimulator
SELECT *
FROM Versie v
JOIN Land l on l.id = v.landenid
JOIN Gemeente g on g.versieid = v.id
JOIN Straat s on s.gemeenteid = g.id
