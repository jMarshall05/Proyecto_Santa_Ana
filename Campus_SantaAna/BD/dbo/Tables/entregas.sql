CREATE TABLE [dbo].[entregas] (
    [id_entrega]        INT            IDENTITY (1, 1) NOT NULL,
    [id_tarea]          INT            NOT NULL,
    [id_estudiante]     NVARCHAR (128) NOT NULL,
    [archivo_entregado] VARCHAR (255)  NULL,
    [fecha_entrega]     DATETIME       NULL,
    [estado]            VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([id_entrega] ASC),
    CONSTRAINT [FK_entregas_tareas] FOREIGN KEY ([id_tarea]) REFERENCES [dbo].[tareas] ([id_tarea]),
    CONSTRAINT [FK_entregas_usuarios] FOREIGN KEY ([id_estudiante]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario])
);

