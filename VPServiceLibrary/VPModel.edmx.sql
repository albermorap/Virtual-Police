
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2014 19:30:51
-- Generated from EDMX file: C:\Users\Alberto\documents\visual studio 2013\Projects\Comisaria\VPServiceLibrary\VPModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DECIMA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CiudadanoAgente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Agentes] DROP CONSTRAINT [FK_CiudadanoAgente];
GO
IF OBJECT_ID(N'[dbo].[FK_RoboCocheVehiculo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_RoboCoche] DROP CONSTRAINT [FK_RoboCocheVehiculo];
GO
IF OBJECT_ID(N'[dbo].[FK_DenunciaExpediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias] DROP CONSTRAINT [FK_DenunciaExpediente];
GO
IF OBJECT_ID(N'[dbo].[FK_ExpedienteCiudadano]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expedientes] DROP CONSTRAINT [FK_ExpedienteCiudadano];
GO
IF OBJECT_ID(N'[dbo].[FK_ActividadAgente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Actividades] DROP CONSTRAINT [FK_ActividadAgente];
GO
IF OBJECT_ID(N'[dbo].[FK_DesaparicionCiudadano]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_Desaparicion] DROP CONSTRAINT [FK_DesaparicionCiudadano];
GO
IF OBJECT_ID(N'[dbo].[FK_SecuestroCiudadano]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_Secuestro] DROP CONSTRAINT [FK_SecuestroCiudadano];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculoCiudadano]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehiculos] DROP CONSTRAINT [FK_VehiculoCiudadano];
GO
IF OBJECT_ID(N'[dbo].[FK_DenunciaAgente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias] DROP CONSTRAINT [FK_DenunciaAgente];
GO
IF OBJECT_ID(N'[dbo].[FK_FotoExpediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expedientes] DROP CONSTRAINT [FK_FotoExpediente];
GO
IF OBJECT_ID(N'[dbo].[FK_FotoDesaparicion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_Desaparicion] DROP CONSTRAINT [FK_FotoDesaparicion];
GO
IF OBJECT_ID(N'[dbo].[FK_CiudadanoDenuncia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias] DROP CONSTRAINT [FK_CiudadanoDenuncia];
GO
IF OBJECT_ID(N'[dbo].[FK_RoboCoche_inherits_Denuncia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_RoboCoche] DROP CONSTRAINT [FK_RoboCoche_inherits_Denuncia];
GO
IF OBJECT_ID(N'[dbo].[FK_Desaparicion_inherits_Denuncia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_Desaparicion] DROP CONSTRAINT [FK_Desaparicion_inherits_Denuncia];
GO
IF OBJECT_ID(N'[dbo].[FK_Secuestro_inherits_Denuncia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias_Secuestro] DROP CONSTRAINT [FK_Secuestro_inherits_Denuncia];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Agentes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agentes];
GO
IF OBJECT_ID(N'[dbo].[Ciudadanos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ciudadanos];
GO
IF OBJECT_ID(N'[dbo].[Denuncias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Denuncias];
GO
IF OBJECT_ID(N'[dbo].[Expedientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expedientes];
GO
IF OBJECT_ID(N'[dbo].[Actividades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actividades];
GO
IF OBJECT_ID(N'[dbo].[Vehiculos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehiculos];
GO
IF OBJECT_ID(N'[dbo].[Fotos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fotos];
GO
IF OBJECT_ID(N'[dbo].[Denuncias_RoboCoche]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Denuncias_RoboCoche];
GO
IF OBJECT_ID(N'[dbo].[Denuncias_Desaparicion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Denuncias_Desaparicion];
GO
IF OBJECT_ID(N'[dbo].[Denuncias_Secuestro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Denuncias_Secuestro];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Agentes'
CREATE TABLE [dbo].[Agentes] (
    [ID_Agente] int IDENTITY(1,1) NOT NULL,
    [NumPlaca] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Rango] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NULL,
    [Estado_Civil] nvarchar(max)  NULL,
    [Fecha_Creacion] datetime  NOT NULL,
    [Observaciones] nvarchar(max)  NULL,
    [Ciudadano_ID_Ciudadano] int  NOT NULL
);
GO

-- Creating table 'Ciudadanos'
CREATE TABLE [dbo].[Ciudadanos] (
    [ID_Ciudadano] int IDENTITY(1,1) NOT NULL,
    [DNI] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellido1] nvarchar(max)  NOT NULL,
    [Apellido2] nvarchar(max)  NOT NULL,
    [Fecha_Nacimiento] datetime  NOT NULL,
    [Sexo] nvarchar(max)  NOT NULL,
    [Calle] nvarchar(max)  NOT NULL,
    [Numero] nvarchar(max)  NOT NULL,
    [Letra_Piso] nvarchar(max)  NULL,
    [Localidad] nvarchar(max)  NOT NULL,
    [Provincia] nvarchar(max)  NOT NULL,
    [CP] nvarchar(max)  NOT NULL,
    [Nacionalidad] nvarchar(max)  NOT NULL,
    [Huella_Dactilar] nvarchar(max)  NULL
);
GO

-- Creating table 'Denuncias'
CREATE TABLE [dbo].[Denuncias] (
    [ID_Denuncia] int IDENTITY(1,1) NOT NULL,
    [Tipo] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [Fecha_Creacion] datetime  NOT NULL,
    [Direccion_Hecho] nvarchar(max)  NOT NULL,
    [Naturaleza_Lugar_Hecho] nvarchar(max)  NOT NULL,
    [Fecha_Hecho] datetime  NOT NULL,
    [Hora_Hecho] time  NOT NULL,
    [Decripcion_Hecho] nvarchar(max)  NOT NULL,
    [ExpedienteID_Expediente] int  NULL,
    [AgenteID_Agente] int  NOT NULL,
    [CiudadanoID_Denunciante] int  NOT NULL
);
GO

-- Creating table 'Expedientes'
CREATE TABLE [dbo].[Expedientes] (
    [ID_Expediente] int IDENTITY(1,1) NOT NULL,
    [Altura] float  NULL,
    [Peso] float  NULL,
    [Edad] int  NOT NULL,
    [Estado_Civil] nvarchar(max)  NULL,
    [Telefono] nvarchar(max)  NULL,
    [Fecha_Creacion] datetime  NOT NULL,
    [Fecha_UltimaModif] datetime  NOT NULL,
    [Ciudadano_ID_Ciudadano] int  NOT NULL,
    [Foto_Id] int  NULL
);
GO

-- Creating table 'Actividades'
CREATE TABLE [dbo].[Actividades] (
    [ID_Actividad] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Hora_Inicio] time  NOT NULL,
    [Hora_Fin] time  NOT NULL,
    [Tipo] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NULL,
    [AgenteID_Agente] int  NOT NULL
);
GO

-- Creating table 'Vehiculos'
CREATE TABLE [dbo].[Vehiculos] (
    [ID_Vehiculo] int IDENTITY(1,1) NOT NULL,
    [Matricula] nvarchar(max)  NOT NULL,
    [Marca] nvarchar(max)  NOT NULL,
    [Modelo] nvarchar(max)  NOT NULL,
    [Color] nvarchar(max)  NOT NULL,
    [NumBastidor] nvarchar(max)  NOT NULL,
    [CiudadanoID_Ciudadano] int  NOT NULL
);
GO

-- Creating table 'Fotos'
CREATE TABLE [dbo].[Fotos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [Size] bigint  NOT NULL,
    [Type] smallint  NOT NULL
);
GO

-- Creating table 'Denuncias_RoboCoche'
CREATE TABLE [dbo].[Denuncias_RoboCoche] (
    [VehiculoID_Vehiculo] int  NOT NULL,
    [ID_Denuncia] int  NOT NULL
);
GO

-- Creating table 'Denuncias_Desaparicion'
CREATE TABLE [dbo].[Denuncias_Desaparicion] (
    [Descripción_Fisica] nvarchar(max)  NOT NULL,
    [CiudadanoID_Ciudadano] int  NOT NULL,
    [ID_Denuncia] int  NOT NULL,
    [Foto_Id] int  NOT NULL
);
GO

-- Creating table 'Denuncias_Secuestro'
CREATE TABLE [dbo].[Denuncias_Secuestro] (
    [CiudadanoID_Ciudadano] int  NOT NULL,
    [ID_Denuncia] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_Agente] in table 'Agentes'
ALTER TABLE [dbo].[Agentes]
ADD CONSTRAINT [PK_Agentes]
    PRIMARY KEY CLUSTERED ([ID_Agente] ASC);
GO

-- Creating primary key on [ID_Ciudadano] in table 'Ciudadanos'
ALTER TABLE [dbo].[Ciudadanos]
ADD CONSTRAINT [PK_Ciudadanos]
    PRIMARY KEY CLUSTERED ([ID_Ciudadano] ASC);
GO

-- Creating primary key on [ID_Denuncia] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [PK_Denuncias]
    PRIMARY KEY CLUSTERED ([ID_Denuncia] ASC);
GO

-- Creating primary key on [ID_Expediente] in table 'Expedientes'
ALTER TABLE [dbo].[Expedientes]
ADD CONSTRAINT [PK_Expedientes]
    PRIMARY KEY CLUSTERED ([ID_Expediente] ASC);
GO

-- Creating primary key on [ID_Actividad] in table 'Actividades'
ALTER TABLE [dbo].[Actividades]
ADD CONSTRAINT [PK_Actividades]
    PRIMARY KEY CLUSTERED ([ID_Actividad] ASC);
GO

-- Creating primary key on [ID_Vehiculo] in table 'Vehiculos'
ALTER TABLE [dbo].[Vehiculos]
ADD CONSTRAINT [PK_Vehiculos]
    PRIMARY KEY CLUSTERED ([ID_Vehiculo] ASC);
GO

-- Creating primary key on [Id] in table 'Fotos'
ALTER TABLE [dbo].[Fotos]
ADD CONSTRAINT [PK_Fotos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID_Denuncia] in table 'Denuncias_RoboCoche'
ALTER TABLE [dbo].[Denuncias_RoboCoche]
ADD CONSTRAINT [PK_Denuncias_RoboCoche]
    PRIMARY KEY CLUSTERED ([ID_Denuncia] ASC);
GO

-- Creating primary key on [ID_Denuncia] in table 'Denuncias_Desaparicion'
ALTER TABLE [dbo].[Denuncias_Desaparicion]
ADD CONSTRAINT [PK_Denuncias_Desaparicion]
    PRIMARY KEY CLUSTERED ([ID_Denuncia] ASC);
GO

-- Creating primary key on [ID_Denuncia] in table 'Denuncias_Secuestro'
ALTER TABLE [dbo].[Denuncias_Secuestro]
ADD CONSTRAINT [PK_Denuncias_Secuestro]
    PRIMARY KEY CLUSTERED ([ID_Denuncia] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Ciudadano_ID_Ciudadano] in table 'Agentes'
ALTER TABLE [dbo].[Agentes]
ADD CONSTRAINT [FK_CiudadanoAgente]
    FOREIGN KEY ([Ciudadano_ID_Ciudadano])
    REFERENCES [dbo].[Ciudadanos]
        ([ID_Ciudadano])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CiudadanoAgente'
CREATE INDEX [IX_FK_CiudadanoAgente]
ON [dbo].[Agentes]
    ([Ciudadano_ID_Ciudadano]);
GO

-- Creating foreign key on [VehiculoID_Vehiculo] in table 'Denuncias_RoboCoche'
ALTER TABLE [dbo].[Denuncias_RoboCoche]
ADD CONSTRAINT [FK_RoboCocheVehiculo]
    FOREIGN KEY ([VehiculoID_Vehiculo])
    REFERENCES [dbo].[Vehiculos]
        ([ID_Vehiculo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoboCocheVehiculo'
CREATE INDEX [IX_FK_RoboCocheVehiculo]
ON [dbo].[Denuncias_RoboCoche]
    ([VehiculoID_Vehiculo]);
GO

-- Creating foreign key on [ExpedienteID_Expediente] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [FK_DenunciaExpediente]
    FOREIGN KEY ([ExpedienteID_Expediente])
    REFERENCES [dbo].[Expedientes]
        ([ID_Expediente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DenunciaExpediente'
CREATE INDEX [IX_FK_DenunciaExpediente]
ON [dbo].[Denuncias]
    ([ExpedienteID_Expediente]);
GO

-- Creating foreign key on [Ciudadano_ID_Ciudadano] in table 'Expedientes'
ALTER TABLE [dbo].[Expedientes]
ADD CONSTRAINT [FK_ExpedienteCiudadano]
    FOREIGN KEY ([Ciudadano_ID_Ciudadano])
    REFERENCES [dbo].[Ciudadanos]
        ([ID_Ciudadano])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExpedienteCiudadano'
CREATE INDEX [IX_FK_ExpedienteCiudadano]
ON [dbo].[Expedientes]
    ([Ciudadano_ID_Ciudadano]);
GO

-- Creating foreign key on [AgenteID_Agente] in table 'Actividades'
ALTER TABLE [dbo].[Actividades]
ADD CONSTRAINT [FK_ActividadAgente]
    FOREIGN KEY ([AgenteID_Agente])
    REFERENCES [dbo].[Agentes]
        ([ID_Agente])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActividadAgente'
CREATE INDEX [IX_FK_ActividadAgente]
ON [dbo].[Actividades]
    ([AgenteID_Agente]);
GO

-- Creating foreign key on [CiudadanoID_Ciudadano] in table 'Denuncias_Desaparicion'
ALTER TABLE [dbo].[Denuncias_Desaparicion]
ADD CONSTRAINT [FK_DesaparicionCiudadano]
    FOREIGN KEY ([CiudadanoID_Ciudadano])
    REFERENCES [dbo].[Ciudadanos]
        ([ID_Ciudadano])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DesaparicionCiudadano'
CREATE INDEX [IX_FK_DesaparicionCiudadano]
ON [dbo].[Denuncias_Desaparicion]
    ([CiudadanoID_Ciudadano]);
GO

-- Creating foreign key on [CiudadanoID_Ciudadano] in table 'Denuncias_Secuestro'
ALTER TABLE [dbo].[Denuncias_Secuestro]
ADD CONSTRAINT [FK_SecuestroCiudadano]
    FOREIGN KEY ([CiudadanoID_Ciudadano])
    REFERENCES [dbo].[Ciudadanos]
        ([ID_Ciudadano])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SecuestroCiudadano'
CREATE INDEX [IX_FK_SecuestroCiudadano]
ON [dbo].[Denuncias_Secuestro]
    ([CiudadanoID_Ciudadano]);
GO

-- Creating foreign key on [CiudadanoID_Ciudadano] in table 'Vehiculos'
ALTER TABLE [dbo].[Vehiculos]
ADD CONSTRAINT [FK_VehiculoCiudadano]
    FOREIGN KEY ([CiudadanoID_Ciudadano])
    REFERENCES [dbo].[Ciudadanos]
        ([ID_Ciudadano])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculoCiudadano'
CREATE INDEX [IX_FK_VehiculoCiudadano]
ON [dbo].[Vehiculos]
    ([CiudadanoID_Ciudadano]);
GO

-- Creating foreign key on [AgenteID_Agente] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [FK_DenunciaAgente]
    FOREIGN KEY ([AgenteID_Agente])
    REFERENCES [dbo].[Agentes]
        ([ID_Agente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DenunciaAgente'
CREATE INDEX [IX_FK_DenunciaAgente]
ON [dbo].[Denuncias]
    ([AgenteID_Agente]);
GO

-- Creating foreign key on [Foto_Id] in table 'Expedientes'
ALTER TABLE [dbo].[Expedientes]
ADD CONSTRAINT [FK_FotoExpediente]
    FOREIGN KEY ([Foto_Id])
    REFERENCES [dbo].[Fotos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FotoExpediente'
CREATE INDEX [IX_FK_FotoExpediente]
ON [dbo].[Expedientes]
    ([Foto_Id]);
GO

-- Creating foreign key on [Foto_Id] in table 'Denuncias_Desaparicion'
ALTER TABLE [dbo].[Denuncias_Desaparicion]
ADD CONSTRAINT [FK_FotoDesaparicion]
    FOREIGN KEY ([Foto_Id])
    REFERENCES [dbo].[Fotos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FotoDesaparicion'
CREATE INDEX [IX_FK_FotoDesaparicion]
ON [dbo].[Denuncias_Desaparicion]
    ([Foto_Id]);
GO

-- Creating foreign key on [CiudadanoID_Denunciante] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [FK_CiudadanoDenuncia]
    FOREIGN KEY ([CiudadanoID_Denunciante])
    REFERENCES [dbo].[Ciudadanos]
        ([ID_Ciudadano])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CiudadanoDenuncia'
CREATE INDEX [IX_FK_CiudadanoDenuncia]
ON [dbo].[Denuncias]
    ([CiudadanoID_Denunciante]);
GO

-- Creating foreign key on [ID_Denuncia] in table 'Denuncias_RoboCoche'
ALTER TABLE [dbo].[Denuncias_RoboCoche]
ADD CONSTRAINT [FK_RoboCoche_inherits_Denuncia]
    FOREIGN KEY ([ID_Denuncia])
    REFERENCES [dbo].[Denuncias]
        ([ID_Denuncia])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Denuncia] in table 'Denuncias_Desaparicion'
ALTER TABLE [dbo].[Denuncias_Desaparicion]
ADD CONSTRAINT [FK_Desaparicion_inherits_Denuncia]
    FOREIGN KEY ([ID_Denuncia])
    REFERENCES [dbo].[Denuncias]
        ([ID_Denuncia])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Denuncia] in table 'Denuncias_Secuestro'
ALTER TABLE [dbo].[Denuncias_Secuestro]
ADD CONSTRAINT [FK_Secuestro_inherits_Denuncia]
    FOREIGN KEY ([ID_Denuncia])
    REFERENCES [dbo].[Denuncias]
        ([ID_Denuncia])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------