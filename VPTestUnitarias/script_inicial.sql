SET IDENTITY_INSERT [dbo].[Ciudadanos] ON
INSERT INTO [dbo].[Ciudadanos] ([ID_Ciudadano], [DNI], [Nombre], [Apellido1], [Apellido2], [Fecha_Nacimiento], [Sexo], [Calle], [Numero], [Letra_Piso], [Localidad], [Provincia], [CP], [Nacionalidad], [Huella_Dactilar]) VALUES (1, N'06284403K', N'Javier', N'Pérez', N'Gómez', convert(datetime,'1990-10-29 00:00:00', 20), N'H', N'Angel', N'25', NULL, N'Albacete', N'Albacete', N'02002', N'Española', NULL)
INSERT INTO [dbo].[Ciudadanos] ([ID_Ciudadano], [DNI], [Nombre], [Apellido1], [Apellido2], [Fecha_Nacimiento], [Sexo], [Calle], [Numero], [Letra_Piso], [Localidad], [Provincia], [CP], [Nacionalidad], [Huella_Dactilar]) VALUES (2, N'06284403L', N'Luis', N'López', N'Granado', convert(datetime,'1985-09-25 00:00:00', 20), N'H', N'calle1', N'35', NULL, N'Toledo', N'Toledo', N'25148', N'Española', NULL)
INSERT INTO [dbo].[Ciudadanos] ([ID_Ciudadano], [DNI], [Nombre], [Apellido1], [Apellido2], [Fecha_Nacimiento], [Sexo], [Calle], [Numero], [Letra_Piso], [Localidad], [Provincia], [CP], [Nacionalidad], [Huella_Dactilar]) VALUES (3, N'22222222A', N'Sara', N'Lopez', N'Ruiz', convert(datetime,'1985-09-25 00:00:00', 20), N'M', N'Jaen', N'3', N'R', N'Cuenca', N'Cuenca', N'34242', N'Española', NULL)
INSERT INTO [dbo].[Ciudadanos] ([ID_Ciudadano], [DNI], [Nombre], [Apellido1], [Apellido2], [Fecha_Nacimiento], [Sexo], [Calle], [Numero], [Letra_Piso], [Localidad], [Provincia], [CP], [Nacionalidad], [Huella_Dactilar]) VALUES (4, N'44444444A', N'Sonia', N'Simarro', N'Perez', convert(datetime,'1985-09-25 00:00:00', 20), N'M', N'Avenida de España', N'74', N'C', N'Albacete', N'Albacete ', N'20002', N'Española', NULL)
INSERT INTO [dbo].[Ciudadanos] ([ID_Ciudadano], [DNI], [Nombre], [Apellido1], [Apellido2], [Fecha_Nacimiento], [Sexo], [Calle], [Numero], [Letra_Piso], [Localidad], [Provincia], [CP], [Nacionalidad], [Huella_Dactilar]) VALUES (5, N'55555555A', N'Marta', N'Castillo', N'Redondo', convert(datetime,'1985-09-25 00:00:00', 20), N'M', N'Cid', N'43', NULL, N'La Gineta', N'Albacete', N'03452', N'Española', NULL)
SET IDENTITY_INSERT [dbo].[Ciudadanos] OFF

SET IDENTITY_INSERT [dbo].[Agentes] ON
INSERT INTO [dbo].[Agentes] ([ID_Agente], [NumPlaca], [Password], [Email], [Rango], [Telefono], [Estado_Civil], [Fecha_Creacion], [Observaciones], [Ciudadano_ID_Ciudadano]) VALUES (1, N'1234K', N'1234K', N'aaa@gmail.com', N'Policia', NULL, NULL, convert(datetime,'2014-10-29 16:33:33', 20), NULL, 1)
INSERT INTO [dbo].[Agentes] ([ID_Agente], [NumPlaca], [Password], [Email], [Rango], [Telefono], [Estado_Civil], [Fecha_Creacion], [Observaciones], [Ciudadano_ID_Ciudadano]) VALUES (2, N'1234L', N'1234L', N'aaa@gmail.com', N'Comisario', NULL, NULL, convert(datetime,'2014-10-29 16:33:56', 20), NULL, 2)
SET IDENTITY_INSERT [dbo].[Agentes] OFF

SET IDENTITY_INSERT [dbo].[Actividades] ON
INSERT INTO [dbo].[Actividades] ([ID_Actividad], [Fecha], [Hora_Inicio], [Hora_Fin], [Tipo], [Descripcion], [AgenteID_Agente]) VALUES (1, convert(datetime,'2014-10-24 00:00:00', 20), N'09:30:00', N'10:00:00', N'Oficina', NULL, 1)
INSERT INTO [dbo].[Actividades] ([ID_Actividad], [Fecha], [Hora_Inicio], [Hora_Fin], [Tipo], [Descripcion], [AgenteID_Agente]) VALUES (2, convert(datetime,'2014-10-24 00:00:00', 20), N'15:20:00', N'18:30:00', N'Patrullar', NULL, 1)
INSERT INTO [dbo].[Actividades] ([ID_Actividad], [Fecha], [Hora_Inicio], [Hora_Fin], [Tipo], [Descripcion], [AgenteID_Agente]) VALUES (3, convert(datetime,'2014-10-26 00:00:00', 20), N'14:25:00', N'17:25:00', N'Emergencias', NULL, 1)
INSERT INTO [dbo].[Actividades] ([ID_Actividad], [Fecha], [Hora_Inicio], [Hora_Fin], [Tipo], [Descripcion], [AgenteID_Agente]) VALUES (4, convert(datetime,'2014-10-26 00:00:00', 20), N'09:00:00', N'14:00:00', N'Oficina', NULL, 2)
SET IDENTITY_INSERT [dbo].[Actividades] OFF

SET IDENTITY_INSERT [dbo].[Expedientes] ON
INSERT INTO [dbo].[Expedientes] ([ID_Expediente], [Altura], [Peso], [Edad], [Estado_Civil], [Telefono], [Fecha_Creacion], [Fecha_UltimaModif], [Ciudadano_ID_Ciudadano], [Foto_Id]) VALUES (1, 5, 5, 5, N'aaa', N'555', convert(datetime,'2014-11-25 00:00:00',20), convert(datetime,'2014-12-04 00:00:00',20), 1, NULL)
INSERT INTO [dbo].[Expedientes] ([ID_Expediente], [Altura], [Peso], [Edad], [Estado_Civil], [Telefono], [Fecha_Creacion], [Fecha_UltimaModif], [Ciudadano_ID_Ciudadano], [Foto_Id]) VALUES (2, 3, 3, 3, N'bbb', N'333', convert(datetime,'2014-11-25 00:00:00',20), convert(datetime,'2014-12-04 00:00:00',20), 2, NULL)
SET IDENTITY_INSERT [dbo].[Expedientes] OFF

SET IDENTITY_INSERT [dbo].[Denuncias] ON
INSERT INTO [dbo].[Denuncias] ([ID_Denuncia], [Tipo], [Estado], [Fecha_Creacion], [Direccion_Hecho], [Naturaleza_Lugar_Hecho], [Fecha_Hecho], [Hora_Hecho], [Decripcion_Hecho], [ExpedienteID_Expediente], [AgenteID_Agente], [CiudadanoID_Denunciante]) VALUES (1, N'Daños', N'Activa', convert(datetime,'2014-12-01 00:00:00',20), N'dir', N'Espacios abiertos', convert(datetime,'2014-12-04 00:00:00',20),  N'05:05:00', N'des', NULL, 1, 2)
INSERT INTO [dbo].[Denuncias] ([ID_Denuncia], [Tipo], [Estado], [Fecha_Creacion], [Direccion_Hecho], [Naturaleza_Lugar_Hecho], [Fecha_Hecho], [Hora_Hecho], [Decripcion_Hecho], [ExpedienteID_Expediente], [AgenteID_Agente], [CiudadanoID_Denunciante]) VALUES (2, N'Daños', N'Activa', convert(datetime,'2014-12-04 00:00:00',20), N'dir', N'Espacios abiertos', convert(datetime,'2014-12-04 00:00:00',20),  N'03:03:00', N'descr', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Denuncias] OFF

