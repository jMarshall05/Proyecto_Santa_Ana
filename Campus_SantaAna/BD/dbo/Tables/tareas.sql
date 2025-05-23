﻿CREATE TABLE [dbo].[tareas] (
    [id_tarea]        INT           IDENTITY (1, 1) NOT NULL,
    [titulo]          VARCHAR (150) NOT NULL,
    [descripcion]     TEXT          NULL,
    [fecha_entrega]   DATETIME      NULL,
    [id_materia]      INT           NOT NULL,
    [archivo_adjunto] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([id_tarea] ASC),
    CONSTRAINT [FK_tareas_materias] FOREIGN KEY ([id_materia]) REFERENCES [dbo].[materias] ([id_materia])
);

