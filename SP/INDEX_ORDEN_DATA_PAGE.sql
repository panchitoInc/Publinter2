
alter PROCEDURE INDEX_ORDEN_DATA_PAGE
	@UsuarioId int,
	@desde int,
	@cantidad int,
	@sortcolumn int,
	@sortdirection varchar(4),
	@anuncianteId int,
	@campaniaId int,
	@medioId int,
	@search varchar(50)
AS
BEGIN

	SELECT 
		O.OrdenId as 'OrdenId',
		CONVERT(datetime, O.Emision) as 'Emision',
		CONCAT('Orden ', O.NroOrden) as 'Descripcion',
		M.Nombre as 'Medio',
		Cli.Nombre as 'Cliente',
		A.Nombre as 'Anunciante',
		C.Nombre as 'Campania',
		O.Total as 'Total',
		O.Anulada,
		O.AnuladaPor,
		'TotalRows' =  COUNT(*) OVER()  --Obtiene el count de todo, antes del offset fetch next
	FROM 
		Orden O
		LEFT JOIN Campania C ON O.CampaniaId = C.CampaniaId
		LEFT JOIN Medio M ON O.MedioId = M.MedioId
		LEFT JOIN Cliente Cli ON C.ClienteId = Cli.ClienteId
		LEFT JOIN Anunciante A ON C.AnuncianteId = A.AnuncianteId 
	WHERE
		(O.UsuarioId = @UsuarioId) AND		
		(@anuncianteId = 0 OR @anuncianteId = A.AnuncianteId) AND
		(@medioId = 0 OR @medioId = M.MedioId) AND
		(@campaniaId = 0 OR @campaniaId = C.CampaniaId) AND
		(
			(@search = '') OR
			(CONVERT(datetime, O.Emision) LIKE '%' + @search + '%') OR 
			(CONCAT('Orden ', O.NroOrden) LIKE '%' + @search + '%') OR
			(CONVERT(varchar, O.Total) LIKE '%' + @search + '%') OR
			A.Nombre LIKE '%' + @search + '%' OR
			C.Nombre LIKE '%' + @search + '%' OR
			M.Nombre LIKE '%' + @search + '%'
		) 

	ORDER BY 
		
		CASE WHEN @sortcolumn = 0 AND @sortdirection = 'asc' THEN CONVERT(datetime, O.Emision) END,
		CASE WHEN @sortcolumn = 1 AND @sortdirection = 'asc' THEN CONCAT('Orden ', O.NroOrden) END,
		CASE WHEN @sortcolumn = 2 AND @sortdirection = 'asc' THEN M.Nombre END,
		CASE WHEN @sortcolumn = 3 AND @sortdirection = 'asc' THEN a.Nombre END,
		CASE WHEN @sortcolumn = 4 AND @sortdirection = 'asc' THEN c.Nombre END,
		CASE WHEN @sortcolumn = 5 AND @sortdirection = 'asc' THEN O.Total END,
		CASE WHEN @sortcolumn = 0 AND @sortdirection = 'desc' THEN CONVERT(datetime, O.Emision) END DESC,
		CASE WHEN @sortcolumn = 1 AND @sortdirection = 'desc' THEN CONCAT('Orden ', O.NroOrden) END DESC,
		CASE WHEN @sortcolumn = 2 AND @sortdirection = 'desc' THEN M.Nombre END DESC,
		CASE WHEN @sortcolumn = 3 AND @sortdirection = 'desc' THEN a.Nombre END DESC,
		CASE WHEN @sortcolumn = 4 AND @sortdirection = 'desc' THEN c.Nombre END DESC,
		CASE WHEN @sortcolumn = 5 AND @sortdirection = 'desc' THEN O.Total END DESC

	OFFSET @desde ROWS
	FETCH NEXT @cantidad ROWS ONLY;
		
END
