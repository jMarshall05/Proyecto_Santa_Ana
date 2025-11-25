CREATE TABLE [dbo].[calificaciones] (
    [id_calificacion]    INT            IDENTITY (1, 1) NOT NULL,
    [id_entrega]         INT            NOT NULL,
    [calificacion]       DECIMAL (5, 2) NULL,
    [comentario]         TEXT           NULL,
    [fecha_calificacion] DATETIME       NULL,
    [estado]             BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_calificacion] ASC),
    CONSTRAINT [CK_calificaciones_estado] CHECK ([estado]=(1) OR [estado]=(0)),
    CONSTRAINT [FK_calificaciones_entrega] FOREIGN KEY ([id_entrega]) REFERENCES [dbo].[entregas] ([id_entrega]) ON DELETE CASCADE
);

