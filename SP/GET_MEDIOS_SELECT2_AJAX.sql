CREATE PROCEDURE GET_MEDIOS_SELECT2_AJAX
(
	@SEARCH nvarchar(max),
	@START int,
	@LENGTH int
)
AS
BEGIN

	SELECT 
		MedioId,
		Nombre,
		Descripcion,
		TipoMedioId,
		'TotalRows' =  COUNT(*) OVER()
	FROM
		Medio
	WHERE
		@SEARCH = '' OR
		(UPPER(Nombre + Descripcion ) COLLATE Latin1_General_CI_AI LIKE '%'+UPPER(@search)+'%' COLLATE Latin1_General_CI_AI)
	ORDER BY Nombre
	OFFSET @START ROWS
	FETCH NEXT @LENGTH ROWS ONLY
END
GO



select * from medio