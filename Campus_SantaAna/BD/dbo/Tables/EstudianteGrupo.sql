CREATE TABLE [dbo].[EstudianteGrupo]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Estudiante_id] NVARCHAR(128) NOT NULL, 
    [Grupo_id] INT NOT NULL
    CONSTRAINT [FK_Usuarios_Grupos] FOREIGN KEY (Grupo_id) REFERENCES [dbo].[grupos] ([id_grupo]),
    CONSTRAINT [FK_EstudianteGrupo_Usuarios] FOREIGN KEY (Estudiante_id) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario])
)

