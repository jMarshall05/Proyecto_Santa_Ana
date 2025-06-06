CREATE TABLE [dbo].[materias] (
    [id_materia]  INT           IDENTITY (1, 1) NOT NULL,
    [nombre]      VARCHAR (100) NOT NULL,
    [descripcion] TEXT          NULL,
    [Id_profesor] NVARCHAR(128) NULL, 
    PRIMARY KEY CLUSTERED ([id_materia] ASC), 
    CONSTRAINT [FK_materias_Usuario] FOREIGN KEY (Id_profesor) REFERENCES [dbo].Usuarios_tb ([IdUsuario]) 
);

