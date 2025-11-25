CREATE TABLE [dbo].[anuncios] (
    [id_anuncio]        INT           IDENTITY (1, 1) NOT NULL,
    [titulo]            VARCHAR (150) NULL,
    [descripcion]       TEXT          NULL,
    [fecha_evento]      DATETIME      NULL,
    [fecha_publicacion] DATETIME      NULL,
    [imagen_ruta]       VARCHAR (300) NULL,
    [estado]            BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_anuncio] ASC),
    CONSTRAINT [CK_anuncios_estado] CHECK ([estado]=(1) OR [estado]=(0))
);

