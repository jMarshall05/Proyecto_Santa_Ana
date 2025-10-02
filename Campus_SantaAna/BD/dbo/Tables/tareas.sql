CREATE TABLE [dbo].[tareas] (
    [id_tarea]           INT            IDENTITY (1, 1) NOT NULL,
    [titulo]             VARCHAR (150)  NOT NULL,
    [descripcion]        TEXT           NULL,
    [fecha_entrega]      DATETIME       NULL,
    [id_materia]         INT            NULL,
    [archivo_adjunto]    VARCHAR (255)  NULL,
    [fecha_modificacion] DATETIME       NULL,
    [FechaPublicacion]   DATETIME       NULL,
    [IdGrupo]            INT            NULL,
    [asignado_por]       NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([id_tarea] ASC),
    CONSTRAINT [FK_tareas_grupo] FOREIGN KEY ([IdGrupo]) REFERENCES [dbo].[grupos] ([id_grupo]) ON DELETE CASCADE,
    CONSTRAINT [FK_tareas_materia] FOREIGN KEY ([id_materia]) REFERENCES [dbo].[materias] ([id_materia]) ON DELETE CASCADE,
    CONSTRAINT [FK_tareas_Usuarios_tb] FOREIGN KEY ([asignado_por]) REFERENCES [dbo].[Usuarios_tb] ([IdUsuario])
);

