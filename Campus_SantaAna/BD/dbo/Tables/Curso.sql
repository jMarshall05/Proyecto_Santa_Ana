CREATE TABLE [dbo].[Curso] (
    [IdCurso]   INT IDENTITY (1, 1) NOT NULL,
    [MateriaId] INT NOT NULL,
    [GrupoId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCurso] ASC),
    CONSTRAINT [FK_Curso_Grupo] FOREIGN KEY ([GrupoId]) REFERENCES [dbo].[grupos] ([id_grupo]) ON DELETE CASCADE,
    CONSTRAINT [FK_Curso_Materia] FOREIGN KEY ([MateriaId]) REFERENCES [dbo].[materias] ([id_materia]) ON DELETE CASCADE
);

