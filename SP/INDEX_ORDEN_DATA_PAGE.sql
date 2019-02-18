
CREATE PROCEDURE INDEX_ORDEN_DATA_PAGE
	@UsuarioId int,
	@desde int,
	@cantidad int,
	@sortcolumn int,
	@sortdirection varchar(4),
	@search varchar(50)
AS
BEGIN

	SELECT 
		O.OrdenId as 'ComprobanteId',
		CONVERT(datetime, O.Emision) as 'Emision',
		CONCAT('Orden ', O.NroOrden) as 'Descripcion',
		C.Nombre as 'Campania',
		O.Total as 'Total',
		'TotalRows' =  COUNT(*) OVER()  --Obtiene el count de todo, antes del offset fetch next
	FROM 
		Orden O
		LEFT JOIN Campania C ON O.CampaniaId = C.CampaniaId
	WHERE
		(O.UsuarioId = @UsuarioId) AND		
		(
			(@search = '') OR
			(CONVERT(datetime, O.Emision) LIKE '%' + @search + '%') OR 
			(CONCAT('Orden ', O.NroOrden) LIKE '%' + @search + '%') OR
			(CONVERT(varchar, O.Total) LIKE '%' + @search + '%') 
		) 

	ORDER BY 
		CASE WHEN @sortcolumn = 1 AND @sortdirection = 'asc' THEN CONVERT(datetime, O.Emision) END,
		CASE WHEN @sortcolumn = 2 AND @sortdirection = 'asc' THEN CONCAT('Orden ', O.NroOrden) END,
		CASE WHEN @sortcolumn = 3 AND @sortdirection = 'asc' THEN C.Nombre END,
		CASE WHEN @sortcolumn = 4 AND @sortdirection = 'asc' THEN O.Total END,
		CASE WHEN @sortcolumn = 1 AND @sortdirection = 'desc' THEN CONVERT(datetime, O.Emision) END DESC,
		CASE WHEN @sortcolumn = 2 AND @sortdirection = 'desc' THEN CONCAT('Orden ', O.NroOrden) END DESC,
		CASE WHEN @sortcolumn = 3 AND @sortdirection = 'desc' THEN C.Nombre END DESC,
		CASE WHEN @sortcolumn = 4 AND @sortdirection = 'desc' THEN O.Total END DESC

	OFFSET @desde ROWS
	FETCH NEXT @cantidad ROWS ONLY;
		
END
