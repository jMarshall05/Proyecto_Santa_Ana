CREATE TABLE [dbo].[EstudianteGrupo] (
    [IdEstudianteGrupo] INT            IDENTITY (1, 1) NOT NULL,
    [EstudianteId]      NVARCHAR (128) NOT NULL,
    [GrupoId]           INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEstudianteGrupo] ASC),
    CONSTRAINT [FK_EstudianteGrupo_Estudiante] FOREIGN KEY ([EstudianteId]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario]) ON DELETE CASCADE,
    CONSTRAINT [FK_EstudianteGrupo_Grupo] FOREIGN KEY ([GrupoId]) REFERENCES [dbo].[grupos] ([id_grupo])
);

