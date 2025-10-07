CREATE TABLE [dbo].[Eventos] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Titulo]      NVARCHAR (255) NOT NULL,
    [FechaInicio] DATETIME       NOT NULL,
    [FechaFin]    DATETIME       NOT NULL,
    [IdUsuario]   NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

