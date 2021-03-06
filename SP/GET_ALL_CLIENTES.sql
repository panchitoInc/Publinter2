

-- Devuelve los clientes con su pronmer contacoto
create PROCEDURE GET_ALL_CLIENTES
AS
BEGIN -- exec GET_ALL_CLIENTES
	  
;WITH CliCon AS
(
   SELECT 
   	  C.ClienteId,
	  C.Nombre,
	  --C.RazonSocial,
	  --C.RUT,
	  Con.Email,
	  Con.Telefono,
      ROW_NUMBER() OVER (PARTITION BY ClienteId ORDER BY ClienteId DESC) AS rn
   FROM Cliente C
   left Join Contacto Con on Con.Cliente_ClienteId = C.ClienteId
)
SELECT *
FROM CliCon
WHERE rn = 1
END
GO
