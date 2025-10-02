CREATE TABLE [dbo].[Telefonos] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Id_Usuario]  NVARCHAR (128) NOT NULL,
    [Codigo_area] INT            NOT NULL,
    [Telefono]    INT            NOT NULL,
    [Tipo]        NVARCHAR (15)  NOT NULL,
    [Estado]      BIT            NOT NULL,
    CONSTRAINT [PK_Telefonos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Telefonos_Usuarios_tb] FOREIGN KEY ([Id_Usuario]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario])
);

