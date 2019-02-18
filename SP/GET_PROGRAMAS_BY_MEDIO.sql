CREATE PROCEDURE GET_PROGRAMAS_BY_MEDIO
	@MEDIOID int = 0
AS
BEGIN
	
	SELECT
		P.ProgramaId,
		P.Nombre,
		P.PrecioSegundo as Precio
	FROM 
		PROGRAMA P
	WHERE
		Medio_MedioId = @MEDIOID
END