CREATE TABLE [dbo].[materias] (
    [id_materia] INT           IDENTITY (1, 1) NOT NULL,
    [nombre]     VARCHAR (100) NOT NULL,
    [estado]     BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id_materia] ASC),
    CONSTRAINT [CK_materias_estado] CHECK ([estado]=(1) OR [estado]=(0))
);

