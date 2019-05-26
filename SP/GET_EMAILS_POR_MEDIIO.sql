
CREATE PROCEDURE GET_EMAILS_POR_MEDIIO
(
	@MedioId int
)
AS
BEGIN
	select c.Email 
	from Medio m
	join Contacto c on c.Medio_MedioId = m.MedioId
	where 
	m.MedioId = @MedioId

END
