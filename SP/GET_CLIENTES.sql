
CREATE PROCEDURE GET_CLIENTES
	@USUARIOID int = 0
AS
BEGIN
	
	SELECT
		C.ClienteId,
		C.Nombre
	FROM 
		CLIENTE C
END
