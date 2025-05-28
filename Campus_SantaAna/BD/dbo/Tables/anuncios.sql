CREATE TABLE [dbo].[anuncios] (
    [id_anuncio]        INT           IDENTITY (1, 1) NOT NULL,
    [titulo]            VARCHAR (150) NULL,
    [descripcion]       TEXT          NULL,
    [fecha_evento]      DATETIME      NULL,
    [fecha_publicacion] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([id_anuncio] ASC)
);

