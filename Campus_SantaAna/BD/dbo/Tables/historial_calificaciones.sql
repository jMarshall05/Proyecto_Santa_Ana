CREATE TABLE [dbo].[historial_calificaciones] (
    [id_historial]    INT            IDENTITY (1, 1) NOT NULL,
    [id_calificacion] INT            NOT NULL,
    [id_estudiante]   NVARCHAR (128) NOT NULL,
    [id_materia]      INT            NOT NULL,
    [fecha_registro]  DATETIME       NULL,
    [comentario]      TEXT           NULL,
    PRIMARY KEY CLUSTERED ([id_historial] ASC),
    CONSTRAINT [FK_historial_calificacion] FOREIGN KEY ([id_calificacion]) REFERENCES [dbo].[calificaciones] ([id_calificacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_historial_estudiante] FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario]),
    CONSTRAINT [FK_historial_materia] FOREIGN KEY ([id_materia]) REFERENCES [dbo].[materias] ([id_materia])
);

