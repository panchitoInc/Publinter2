
Create procedure GET_ALL_CAMPANIA
as
begin

select Camp.CampaniaId,
	   Camp.Nombre,
	   Cli.ClienteId,
	   Cli.Nombre as ClienteNombre,
	   A.AnuncianteId,
	   A.Nombre as AnuncianteNombre
from 
	Campania Camp
	JOIN Cliente Cli on Cli.ClienteId = Camp.ClienteId
	JOIN Anunciante A on A.AnuncianteId = Camp.AnuncianteId  

end

