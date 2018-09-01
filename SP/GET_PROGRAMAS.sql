
CREATE PROCEDURE GET_PROGRAMAS
	@USUARIOID int = 0
AS
BEGIN
	
	SELECT
		P.ProgramaId,
		P.Nombre,
		P.PrecioSegundo as Precio
	FROM 
		PROGRAMA P
END