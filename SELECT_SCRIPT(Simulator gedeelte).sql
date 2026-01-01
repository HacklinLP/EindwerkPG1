select *
from SimulatieInfo si
join SimulatieInstellingen sinst on sinst.siminfoid = si.id
join SimulatieKlant sk on sk.siminfoid = si.id