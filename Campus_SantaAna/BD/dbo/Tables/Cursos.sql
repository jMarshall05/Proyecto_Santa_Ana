CREATE TABLE [dbo].[Cursos] (
    [IdCurso]    INT            IDENTITY (1, 1) NOT NULL,
    [MateriaId]  INT            NOT NULL,
    [IdProfesor] NVARCHAR (128) NULL,
    [GrupoId]    INT            NOT NULL,
    CONSTRAINT [PK__Curso__085F27D6BB363355] PRIMARY KEY CLUSTERED ([IdCurso] ASC),
    CONSTRAINT [FK_Cursos_Grupo] FOREIGN KEY ([GrupoId]) REFERENCES [dbo].[grupos] ([id_grupo]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cursos_Materia] FOREIGN KEY ([MateriaId]) REFERENCES [dbo].[materias] ([id_materia]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cursos_Profesor] FOREIGN KEY ([IdProfesor]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario]) ON DELETE SET NULL
);

