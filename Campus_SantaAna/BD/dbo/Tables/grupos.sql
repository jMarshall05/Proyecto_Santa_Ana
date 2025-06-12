CREATE TABLE [dbo].[grupos] (
    [id_grupo]           INT            IDENTITY (1, 1) NOT NULL,
    [nombre_grupo] VARCHAR (100)  NOT NULL,
    [descripcion]  TEXT           NOT NULL,
    [creado_por]   NVARCHAR (128) NOT NULL,
    [estado]       BIT   NOT NULL,
    [FechaDeCreacion] DATETIME NOT NULL, 
    [FechaDeModificacion] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([id_grupo] ASC)
);

