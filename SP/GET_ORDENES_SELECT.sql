CREATE PROCEDURE GET_ORDENES_SELECT
	@CAMPANIAID int = 0,
	@MEDIOID int = 0
AS
BEGIN

	SELECT 
		O.OrdenId,
		CONCAT('Orden N° ', O.NroOrden) as Descripcion
	FROM
		Orden O
	WHERE
		O.CampaniaId = @CAMPANIAID AND
		O.MedioId = @MEDIOID
END