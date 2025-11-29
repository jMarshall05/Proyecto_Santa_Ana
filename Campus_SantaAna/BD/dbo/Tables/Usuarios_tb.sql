CREATE TABLE [dbo].[Usuarios_tb] (
    [IdUsuario]           NVARCHAR (128) NOT NULL,
    [Nombre]              VARCHAR (25)   NOT NULL,
    [Apellido]            VARCHAR (25)   NOT NULL,
    [Email]               NVARCHAR (100) NOT NULL,
    [FechaDeNacimiento]   DATETIME       NOT NULL,
    [Identificacion]              INT            NOT NULL,
    [FechaDeRegistro]     DATETIME       NOT NULL,
    [FechaDeModificacion] DATETIME       NULL,
    [Rol]                 NVARCHAR (128) NOT NULL,
    [Estado]              BIT            NOT NULL,
    [TipoIdentificacion]  NVARCHAR (50)  NULL,
    CONSTRAINT [PK_UsuariosID] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_Usuarios_AspNetUsers] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([Identificacion] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

