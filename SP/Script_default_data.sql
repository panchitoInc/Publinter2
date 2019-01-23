
insert into Rol values('Duenio')
insert into Usuario values(1,'aOliva','1234','Antonio','Oliva')


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
insert into medio values('Del Sol FM', 'Radio Del Sol', 2, 10)
insert into Contacto values('delsol@delsol.com','092687567','18 de Julio 0000','Montevideo','Montevideo','Rafael Cotelo',NULL,NULL,3,NULL)

insert into medio values('Canal 10', 'El canal de los uruguayos', 1, 20)
insert into Contacto values('canal10@saeta.com','099789543','Soriano 1234','Montevideo','Montevideo','Blanca Rodriguez',NULL,NULL,4,NULL)

-- Programas
insert into programa values('No Toquen Nada','2018-08-16 08:00:00.000', 180, 40.00000000, 3)
insert into programa values('La Mesa de los Galanes', '2018-08-16 13:00:00.000', 180, 45.00000000, 3)
insert into programa values('Subrayado', '2018-08-16 13:00:00.000', 90, 70.00000000, 4)
insert into programa values('Salven el millon', '2018-08-16 08:00:00.000', 60, 60.00000000, 4)

select * from Contacto

-- Clientes
insert into cliente values('DDB')
insert into Contacto values('ddb@ddb.com','092335444','Colonia 1234','Montevideo','Montevideo','Romina Garcia',NULL,1,NULL,NULL)

insert into cliente values('Birome', 'Birome S.A.', '154367853452')
insert into Contacto values('birome@birome.com','092456789','Arenal Grande 1234','Montevideo','Montevideo','Hernan Faggiani',NULL,2,NULL)

-- Anunciantes
insert into Anunciante values('Barraca europa', 'Barraca europa S.A.', '154367853452')
insert into Contacto values('beu@beu.com','092456789','Arenal Grande 1234','Montevideo','Montevideo','Hernan Faggiani',1,NULL,NULL,NULL)

insert into Anunciante values('Portones PPA', 'PPA SRL', '265678534528')
insert into Contacto values('ppa@ppa.com','09546876','18 de Julio 356','Montevideo','Montevideo','German Rodriguez',2,NULL,NULL,NULL)

-- Campania
insert into Campania values('PPA 2019', 2, 1)
insert into Campania values('Barraca Europa 2019', 1, 2)

-- Materiales
insert into material values('Portones','Mencion toto da silveira',8,4,1)
insert into material values('Cerraduras', 'Comercial largo',30,1,1)
insert into material values('Linea taladros', 'Carteleria taladros',20,1,2)
insert into material values('Pinturas','Promo pintureria',5,3,2)
