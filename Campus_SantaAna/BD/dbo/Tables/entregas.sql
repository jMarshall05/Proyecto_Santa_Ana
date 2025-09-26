CREATE TABLE [dbo].[entregas] (
    [id_entrega]        INT            IDENTITY (1, 1) NOT NULL,
    [id_tarea]          INT            NOT NULL,
    [id_estudiante]     NVARCHAR (128) NOT NULL,
    [archivo_entregado] VARCHAR (255)  NULL,
    [fecha_entrega]     DATETIME       NULL,
    [estado]            BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([id_entrega] ASC),
    CONSTRAINT [FK_entregas_estudiante] FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario]) ON DELETE CASCADE,
    CONSTRAINT [FK_entregas_tarea] FOREIGN KEY ([id_tarea]) REFERENCES [dbo].[tareas] ([id_tarea]) ON DELETE CASCADE
);

