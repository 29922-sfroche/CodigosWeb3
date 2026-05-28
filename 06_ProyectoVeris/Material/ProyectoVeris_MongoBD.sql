USE [master]
GO

IF DB_ID(N'ProyectoVeris_MongoBD') IS NOT NULL
BEGIN
    ALTER DATABASE [ProyectoVeris_MongoBD] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [ProyectoVeris_MongoBD];
END
GO

CREATE DATABASE [ProyectoVeris_MongoBD]
GO

USE [ProyectoVeris_MongoBD]
GO

/* ==========================================================
   TABLA: Especialidades
   ========================================================== */

CREATE TABLE [dbo].[Especialidades](
    [_id] CHAR(24) NOT NULL,
    [descripcion] VARCHAR(50) NOT NULL,
    [dias] VARCHAR(45) NOT NULL,
    [franjaHI] TIME(7) NOT NULL,
    [franjaHF] TIME(7) NOT NULL,
    CONSTRAINT [PK_Especialidades] PRIMARY KEY ([_id])
)
GO

/* ==========================================================
   TABLA: Medicamentos
   ========================================================== */

CREATE TABLE [dbo].[Medicamentos](
    [_id] CHAR(24) NOT NULL,
    [nombre] VARCHAR(50) NOT NULL,
    [tipo] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Medicamentos] PRIMARY KEY ([_id])
)
GO

/* ==========================================================
   TABLA: Medicos
   ========================================================== */

CREATE TABLE [dbo].[Medicos](
    [_id] CHAR(24) NOT NULL,
    [nombre] VARCHAR(50) NOT NULL,
    [especialidadId] CHAR(24) NOT NULL,
    [foto] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Medicos] PRIMARY KEY ([_id]),
    CONSTRAINT [FK_Medicos_Especialidades] FOREIGN KEY ([especialidadId])
        REFERENCES [dbo].[Especialidades] ([_id])
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
GO

/* ==========================================================
   TABLA: Pacientes
   ========================================================== */

CREATE TABLE [dbo].[Pacientes](
    [_id] CHAR(24) NOT NULL,
    [nombre] VARCHAR(50) NOT NULL,
    [cedula] INT NOT NULL,
    [edad] INT NOT NULL,
    [genero] VARCHAR(50) NOT NULL,
    [estatura] INT NOT NULL,
    [peso] FLOAT NOT NULL,
    [foto] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Pacientes] PRIMARY KEY ([_id])
)
GO

/* ==========================================================
   TABLA: Consultas
   ========================================================== */

CREATE TABLE [dbo].[Consultas](
    [_id] CHAR(24) NOT NULL,
    [medicoId] CHAR(24) NOT NULL,
    [pacienteId] CHAR(24) NOT NULL,
    [fechaConsulta] DATE NOT NULL,
    [hi] TIME(7) NOT NULL,
    [hf] TIME(7) NOT NULL,
    [diagnostico] VARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_Consultas] PRIMARY KEY ([_id]),
    CONSTRAINT [FK_Consultas_Medicos] FOREIGN KEY ([medicoId])
        REFERENCES [dbo].[Medicos] ([_id])
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT [FK_Consultas_Pacientes] FOREIGN KEY ([pacienteId])
        REFERENCES [dbo].[Pacientes] ([_id])
)
GO

/* ==========================================================
   TABLA: Recetas
   ========================================================== */

CREATE TABLE [dbo].[Recetas](
    [_id] CHAR(24) NOT NULL,
    [consultaId] CHAR(24) NOT NULL,
    [medicamentoId] CHAR(24) NOT NULL,
    [cantidad] INT NOT NULL,
    CONSTRAINT [PK_Recetas] PRIMARY KEY ([_id]),
    CONSTRAINT [FK_Recetas_Consultas] FOREIGN KEY ([consultaId])
        REFERENCES [dbo].[Consultas] ([_id])
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT [FK_Recetas_Medicamentos] FOREIGN KEY ([medicamentoId])
        REFERENCES [dbo].[Medicamentos] ([_id])
)
GO

/* ==========================================================
   DATOS ORIGINALES CON IDs COMPATIBLES CON OBJECTID
   ========================================================== */

/* Especialidades */
INSERT INTO [dbo].[Especialidades] ([_id], [descripcion], [dias], [franjaHI], [franjaHF])
VALUES ('65a300000000000000000001', 'Cardiologia', 'MJV', CAST('08:00:00' AS TIME), CAST('12:00:00' AS TIME));

INSERT INTO [dbo].[Especialidades] ([_id], [descripcion], [dias], [franjaHI], [franjaHF])
VALUES ('65a300000000000000000002', 'Pediatria', 'LMXJV', CAST('08:00:00' AS TIME), CAST('18:00:00' AS TIME));

INSERT INTO [dbo].[Especialidades] ([_id], [descripcion], [dias], [franjaHI], [franjaHF])
VALUES ('65a300000000000000000003', 'Dermatologia', 'MJ', CAST('12:00:00' AS TIME), CAST('18:00:00' AS TIME));

INSERT INTO [dbo].[Especialidades] ([_id], [descripcion], [dias], [franjaHI], [franjaHF])
VALUES ('65a300000000000000000004', 'Ginecologia', 'LXV', CAST('08:00:00' AS TIME), CAST('18:00:00' AS TIME));

INSERT INTO [dbo].[Especialidades] ([_id], [descripcion], [dias], [franjaHI], [franjaHF])
VALUES ('65a300000000000000000005', 'Oftalmologia', 'LV', CAST('08:00:00' AS TIME), CAST('12:00:00' AS TIME));

INSERT INTO [dbo].[Especialidades] ([_id], [descripcion], [dias], [franjaHI], [franjaHF])
VALUES ('65a300000000000000000006', 'Proctologo', 'LM', CAST('08:00:00' AS TIME), CAST('12:00:00' AS TIME));
GO

/* Medicamentos */
INSERT INTO [dbo].[Medicamentos] ([_id], [nombre], [tipo])
VALUES ('65a300000000000000000101', 'Paracetamol', 'Analgesico');

INSERT INTO [dbo].[Medicamentos] ([_id], [nombre], [tipo])
VALUES ('65a300000000000000000102', 'Amoxicilina', 'Antibiotico');

INSERT INTO [dbo].[Medicamentos] ([_id], [nombre], [tipo])
VALUES ('65a300000000000000000103', 'Loratadina', 'Antihistaminico');

INSERT INTO [dbo].[Medicamentos] ([_id], [nombre], [tipo])
VALUES ('65a300000000000000000104', 'Omeprazol', 'Antiacido');

INSERT INTO [dbo].[Medicamentos] ([_id], [nombre], [tipo])
VALUES ('65a300000000000000000105', 'Aspirina', 'Antiinflamatorio');
GO

/* Médicos */
INSERT INTO [dbo].[Medicos] ([_id], [nombre], [especialidadId], [foto])
VALUES ('65a300000000000000000201', 'Manotas', '65a300000000000000000001', 'usu01.jpg');
GO

/* Pacientes */
INSERT INTO [dbo].[Pacientes] ([_id], [nombre], [cedula], [edad], [genero], [estatura], [peso], [foto])
VALUES ('65a300000000000000000301', 'Plutarco', 1718684408, 18, 'Masculino', 160, 60, 'usu02.jpg');
GO

/* Consultas */
INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000401', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-01-10' AS DATE), CAST('09:00:00' AS TIME), CAST('10:00:00' AS TIME), 'Presion arterial alta');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000402', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-02-15' AS DATE), CAST('14:00:00' AS TIME), CAST('15:00:00' AS TIME), 'Papiloma Humano');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000403', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-03-20' AS DATE), CAST('12:00:00' AS TIME), CAST('13:00:00' AS TIME), 'Problemas cutaneos');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000404', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-04-25' AS DATE), CAST('16:00:00' AS TIME), CAST('17:00:00' AS TIME), 'Examen ocular');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000405', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-05-30' AS DATE), CAST('10:00:00' AS TIME), CAST('11:00:00' AS TIME), 'Chequeo ginecologico');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000406', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-11-20' AS DATE), CAST('08:00:00' AS TIME), CAST('09:00:00' AS TIME), 'Mamografía');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000407', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-11-22' AS DATE), CAST('10:00:00' AS TIME), CAST('11:00:00' AS TIME), 'Arritmias cardíacas');

INSERT INTO [dbo].[Consultas] ([_id], [medicoId], [pacienteId], [fechaConsulta], [hi], [hf], [diagnostico])
VALUES ('65a300000000000000000408', '65a300000000000000000201', '65a300000000000000000301', CAST('2023-11-17' AS DATE), CAST('11:00:00' AS TIME), CAST('12:00:00' AS TIME), 'Hipermetropía');
GO

/* Recetas */
INSERT INTO [dbo].[Recetas] ([_id], [consultaId], [medicamentoId], [cantidad])
VALUES ('65a300000000000000000501', '65a300000000000000000401', '65a300000000000000000101', 2);

INSERT INTO [dbo].[Recetas] ([_id], [consultaId], [medicamentoId], [cantidad])
VALUES ('65a300000000000000000502', '65a300000000000000000402', '65a300000000000000000101', 1);

INSERT INTO [dbo].[Recetas] ([_id], [consultaId], [medicamentoId], [cantidad])
VALUES ('65a300000000000000000503', '65a300000000000000000403', '65a300000000000000000101', 3);

INSERT INTO [dbo].[Recetas] ([_id], [consultaId], [medicamentoId], [cantidad])
VALUES ('65a300000000000000000504', '65a300000000000000000404', '65a300000000000000000101', 1);
GO

/* ==========================================================
   ÍNDICES
   ========================================================== */

CREATE UNIQUE INDEX [IX_Pacientes_cedula]
ON [dbo].[Pacientes] ([cedula])
GO

CREATE INDEX [IX_Medicos_especialidadId]
ON [dbo].[Medicos] ([especialidadId])
GO

CREATE INDEX [IX_Consultas_medicoId]
ON [dbo].[Consultas] ([medicoId])
GO

CREATE INDEX [IX_Consultas_pacienteId]
ON [dbo].[Consultas] ([pacienteId])
GO

CREATE INDEX [IX_Recetas_consultaId]
ON [dbo].[Recetas] ([consultaId])
GO

CREATE INDEX [IX_Recetas_medicamentoId]
ON [dbo].[Recetas] ([medicamentoId])
GO