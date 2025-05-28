CREATE TABLE [dbo].[Usuarios_tb] (
    [IdUsuario]           NVARCHAR (128) NOT NULL,
    [Nombre]              VARCHAR (25)   NOT NULL,
    [Apellido]            VARCHAR (25)   NOT NULL,
    [Email]               NVARCHAR (100) NOT NULL,
    [Telefono]            INT            NOT NULL,
    [FechaDeNacimiento]   DATETIME       NOT NULL,
    [Cedula]              INT            NOT NULL,
    [FechaDeRegistro]     DATETIME       NOT NULL,
    [FechaDeModificacion] DATETIME       NULL,
    [Rol]                 NVARCHAR (128) NOT NULL,
    [Estado]              BIT            NOT NULL,
    CONSTRAINT [PK_UsuariosID] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_Usuarios_AspNetUsers] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([Cedula] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

