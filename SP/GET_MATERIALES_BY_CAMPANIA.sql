
CREATE PROCEDURE GET_MATERIALES_BY_CAMPANIA
	@CAMPANIAID int = 0
AS 
BEGIN

	SELECT
		M.MaterialId,
		M.Titulo as Nombre,
		M.DuracionSegundos as Duracion
	FROM
		Material M
	WHERE 
		M.Campania_CampaniaId = @CAMPANIAID

END
GO