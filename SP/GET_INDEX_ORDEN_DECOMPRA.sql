CREATE PROCEDURE GET_INDEX_ORDEN_DECOMPRA
AS
BEGIN
SELECT 
 oc.*,
 m.Nombre,
 m.PorcentajeDescuento
 from
 OrdenDeCompra oc
 JOIN Medio m on m.MedioId = oc.MedioId

END
