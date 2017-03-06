

SET IDENTITY_INSERT [dbo].[Vehiculos] ON
INSERT INTO [dbo].[Vehiculos] ([ID_Vehiculo], [Matricula], [Marca], [Modelo], [Color], [NumBastidor], [CiudadanoID_Ciudadano]) VALUES (1, N'1234AB', N'Audi', N'A3', N'Rojo', N'45342', 5)
SET IDENTITY_INSERT [dbo].[Vehiculos] OFF


SET IDENTITY_INSERT [dbo].[Denuncias] ON
INSERT INTO [dbo].[Denuncias] ([ID_Denuncia], [Tipo], [Estado], [Fecha_Creacion], [Direccion_Hecho], [Naturaleza_Lugar_Hecho], [Fecha_Hecho], [Hora_Hecho], [Decripcion_Hecho], [ExpedienteID_Expediente], [AgenteID_Agente], [CiudadanoID_Denunciante]) VALUES (3, N'Robo de vehiculo', N'Activa',convert(datetime,'2014-11-25 00:00:00',20), N'Calle Cid', N'aaa',convert(datetime,'2014-11-25 00:00:00',20), N'05:05:05', N'Desaparecio en el barrio san pablo', NULL, 2, 5)

INSERT INTO [dbo].[Denuncias] ([ID_Denuncia], [Tipo], [Estado], [Fecha_Creacion], [Direccion_Hecho], [Naturaleza_Lugar_Hecho], [Fecha_Hecho], [Hora_Hecho], [Decripcion_Hecho], [ExpedienteID_Expediente], [AgenteID_Agente], [CiudadanoID_Denunciante]) VALUES (5, N'Secuestro', N'Activa',convert(datetime,'2014-11-25 00:00:00',20), N'Calle Cid', N'aaa',convert(datetime,'2014-11-25 00:00:00',20), N'05:05:05', N'Desaparecio en el barrio san pablo', NULL, 2, 5)
INSERT INTO [dbo].[Denuncias] ([ID_Denuncia], [Tipo], [Estado], [Fecha_Creacion], [Direccion_Hecho], [Naturaleza_Lugar_Hecho], [Fecha_Hecho], [Hora_Hecho], [Decripcion_Hecho], [ExpedienteID_Expediente], [AgenteID_Agente], [CiudadanoID_Denunciante]) VALUES (6, N'Denuncia', N'Activa',convert(datetime,'2014-11-25 00:00:00',20), N'Calle Cid', N'aaa', convert(datetime,'2014-11-25 00:00:00',20), N'05:05:05', N'Desaparecio en el barrio san pablo', NULL, 2, 5)
INSERT INTO [dbo].[Denuncias] ([ID_Denuncia], [Tipo], [Estado], [Fecha_Creacion], [Direccion_Hecho], [Naturaleza_Lugar_Hecho], [Fecha_Hecho], [Hora_Hecho], [Decripcion_Hecho], [ExpedienteID_Expediente], [AgenteID_Agente], [CiudadanoID_Denunciante]) VALUES (7, N'Denuncia', N'Espera', convert(datetime,'2014-11-25 00:00:00',20), N'calle', N'aa',convert(datetime,'2014-11-25 00:00:00',20), N'04:04:04', N'aa', NULL, 2, 5)
SET IDENTITY_INSERT [dbo].[Denuncias] OFF


INSERT INTO [dbo].[Denuncias_RoboCoche] ([VehiculoID_Vehiculo], [ID_Denuncia]) VALUES (1, 3)
INSERT INTO [dbo].[Denuncias_Secuestro] ([CiudadanoID_Ciudadano], [ID_Denuncia]) VALUES (5, 5)