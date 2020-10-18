
CREATE PROCEDURE GET_ANUNCIANTES

AS
BEGIN
	
	SELECT
		A.AnuncianteId,
		A.Nombre,
		(select top 1 Email from Contacto where Anunciante_AnuncianteId = a.AnuncianteId) as Email,
		(select top 1 Telefono from Contacto where Anunciante_AnuncianteId = a.AnuncianteId) as Telefono
		
	FROM 
		Anunciante A
END
	