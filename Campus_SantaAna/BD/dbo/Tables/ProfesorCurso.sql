CREATE TABLE [dbo].[ProfesorCurso] (
    [IdProfesorCurso] INT            IDENTITY (1, 1) NOT NULL,
    [ProfesorId]      NVARCHAR (128) NOT NULL,
    [CursoId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProfesorCurso] ASC),
    CONSTRAINT [FK_ProfesorCurso_Curso] FOREIGN KEY ([CursoId]) REFERENCES [dbo].[Curso] ([IdCurso]),
    CONSTRAINT [FK_ProfesorCurso_Profesor] FOREIGN KEY ([ProfesorId]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario]) ON DELETE CASCADE
);

