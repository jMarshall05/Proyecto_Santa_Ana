CREATE TABLE [dbo].[Documentos] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Titulo]        NVARCHAR (200) NOT NULL,
    [Descripcion]   NVARCHAR (500) NOT NULL,
    [RutaArchivo]   NVARCHAR (500) NULL,
    [Categoria]     NVARCHAR (100) NOT NULL,
    [FechaRegistro] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

