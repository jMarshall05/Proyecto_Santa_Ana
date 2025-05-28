CREATE TABLE [dbo].[grupos] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [id_grupo]     INT            NOT NULL,
    [nombre_grupo] VARCHAR (100)  NOT NULL,
    [descripcion]  TEXT           NULL,
    [creado_por]   NVARCHAR (128) NOT NULL,
    [id_usuario]   NVARCHAR (128) NOT NULL,
    [estado]       VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

