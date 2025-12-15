CREATE TABLE [dbo].[Usuarios_tb] (
    [IdUsuario]           NVARCHAR (128) NOT NULL,
    [Nombre]              VARCHAR (25)   NOT NULL,
    [Apellido]            VARCHAR (25)   NOT NULL,
    [Email]               NVARCHAR (100) NOT NULL,
    [FechaDeNacimiento]   DATETIME       NOT NULL,
    [Identificacion]      NVARCHAR (50)  NOT NULL,
    [FechaDeRegistro]     DATETIME       NOT NULL,
    [FechaDeModificacion] DATETIME       NULL,
    [Rol]                 NVARCHAR (128) NOT NULL,
    [Estado]              BIT            NOT NULL,
    [TipoIdentificacion]  NVARCHAR (50)  NULL,
    CONSTRAINT [PK_UsuariosID] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_Usuarios_AspNetUsers] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ__Usuarios__A9D10534D67F8533] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [UQ__Usuarios__B4ADFE38B4BFC9C9] UNIQUE NONCLUSTERED ([Identificacion] ASC)
);

