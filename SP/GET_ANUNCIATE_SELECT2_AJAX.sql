CREATE PROCEDURE GET_ANUNCIATE_SELECT2_AJAX
(
	@SEARCH nvarchar(max),
	@START int,
	@LENGTH int
)
AS
BEGIN

	SELECT 
		anuncianteid,
		Nombre,
		RazonSocial,
		RUT,
		'TotalRows' =  COUNT(*) OVER()
	FROM
		Anunciante
	WHERE
		@SEARCH = '' OR
		(UPPER(Nombre + RazonSocial + ISNULL(RUT,'')) COLLATE Latin1_General_CI_AI LIKE '%'+UPPER(@search)+'%' COLLATE Latin1_General_CI_AI)
	ORDER BY Nombre
	OFFSET @START ROWS
	FETCH NEXT @LENGTH ROWS ONLY


END
GO



