CREATE TABLE [dbo].[Bitacora] (
    [IdBitacora]  NVARCHAR (MAX) NOT NULL,
    [Fecha]       DATETIME       CONSTRAINT [DF__Bitacora__Fecha__7AF13DF7] DEFAULT (getdate()) NOT NULL,
    [Usuario]     NVARCHAR (128) NOT NULL,
    [Accion]      NVARCHAR (50)  NOT NULL,
    [Tabla]       NVARCHAR (100) NOT NULL,
    [Descripcion] NVARCHAR (500) NOT NULL
);

