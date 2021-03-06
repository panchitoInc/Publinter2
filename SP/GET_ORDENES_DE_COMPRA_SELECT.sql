
CREATE PROCEDURE GET_ORDENES_DE_COMPRA_SELECT
	@MEDIOID int = 0
AS
BEGIN

	SELECT 
		O.OrdenDeCompraId,
		CONCAT('Orden de compra N° ', O.NumeroDeOrdenDeCompra) as Descripcion,
		O.Saldo
	FROM
		OrdenDeCompra O
	WHERE
		O.MedioId = @MEDIOID AND
		O.Saldo > 0;
END
GO