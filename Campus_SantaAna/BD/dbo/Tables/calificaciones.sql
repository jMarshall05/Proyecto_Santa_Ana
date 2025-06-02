CREATE TABLE [dbo].[calificaciones] (
    [id_calificacion]    INT            IDENTITY (1, 1) NOT NULL,
    [id_entrega]         INT            NOT NULL,
    [calificacion]       DECIMAL (5, 2) NULL,
    [comentario]         TEXT           NULL,
    [fecha_calificacion] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([id_calificacion] ASC),
    CONSTRAINT [FK_calificaciones_entregas] FOREIGN KEY ([id_entrega]) REFERENCES [dbo].[entregas] ([id_entrega])
);

