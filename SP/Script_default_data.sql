-- Tipo Medio
insert into TipoMedio values('Televisión')
insert into TipoMedio values('Radio')
insert into TipoMedio values('Vía publica')
insert into TipoMedio values('Digital')

-- Tipo Material
insert into TipoMaterial values('Spot')
insert into TipoMaterial values('PNT')
insert into TipoMaterial values('Auspicio')
insert into TipoMaterial values('Mencion')

-- Medios
insert into medio values('Del Sol FM', 'Radio Del Sol', 2)
insert into Contacto values('delsol@delsol.com','092687567','18 de Julio 0000','Montevideo','Montevideo','Rafael Cotelo',NULL,1)

insert into medio values('Canal 10', 'El canal de los uruguayos', 1)
insert into Contacto values('canal10@saeta.com','099789543','Soriano 1234','Montevideo','Montevideo','Blanca Rodriguez',NULL,2)

-- Programas
insert into programa values('No Toquen Nada','2018-08-16 08:00:00.000', 180, 40.00000000, 1)
insert into programa values('La Mesa de los Galanes', '2018-08-16 13:00:00.000', 180, 45.00000000, 1)
insert into programa values('Subrayado', '2018-08-16 13:00:00.000', 90, 70.00000000, 2)
insert into programa values('Salven el millon', '2018-08-16 08:00:00.000', 60, 60.00000000, 2)

-- Clientes
insert into cliente values('DDB', 'DDB Uruguay', '234587653654')
insert into Contacto values('ddb@ddb.com','092335444','Colonia 1234','Montevideo','Montevideo','Romina Garcia',1,NULL)

insert into cliente values('Birome', 'Birome S.A.', '154367853452')
insert into Contacto values('birome@birome.com','092456789','Arenal Grande 1234','Montevideo','Montevideo','Hernan Faggiani',2,NULL)

-- Materiales
insert into material values('Bar luz','Mencion bar luz',8,4,1)
insert into material values('Memory', 'Chau papel',30,1,1)
insert into material values('Cablevision', 'Estrenos invierno',20,1,2)
insert into material values('Enxuta','Promo linea blanca',5,3,2)
