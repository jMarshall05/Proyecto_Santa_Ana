CREATE TABLE [dbo].[tareas] (
    [id_tarea]           INT           IDENTITY (1, 1) NOT NULL,
    [titulo]             VARCHAR (150) NOT NULL,
    [descripcion]        TEXT          NULL,
    [fecha_entrega]      DATETIME      NULL,
    [id_materia]         INT           NULL,
    [archivo_adjunto]    VARCHAR (255) NULL,
    [fecha_creacion]     DATETIME      DEFAULT (getdate()) NOT NULL,
    [fecha_modificacion] DATETIME      NULL,
    [FechaPublicacion]   DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([id_tarea] ASC),
    CONSTRAINT [FK_tareas_materias] FOREIGN KEY ([id_materia]) REFERENCES [dbo].[materias] ([id_materia])
);

