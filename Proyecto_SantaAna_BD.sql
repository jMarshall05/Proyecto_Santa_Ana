Create Database Proyecto_SantaAna
Use Proyecto_SantaAna;
drop TABLE Usuarios_tb
CREATE TABLE Usuarios_tb (
    IdUsuario nvarchar(128) not null,
    Nombre varchar(25) not null,
    Apellido varchar(25) not null,
    Email nvarchar(100) unique not null,
    Telefono int not null,
    FechaDeNacimiento datetime not null,
    Cedula int unique not null,
    FechaDeRegistro datetime not null,
    FechaDeModificacion datetime null,
    Rol nvarchar(128) not null,
    Estado bit not null,
    
    CONSTRAINT PK_UsuariosID PRIMARY KEY (IdUsuario),
          
    CONSTRAINT FK_Usuarios_AspNetUsers FOREIGN KEY (IdUsuario)
        REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);


/**Script de Identity**/
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/11/2024 13:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/11/2024 13:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/11/2024 13:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/11/2024 13:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/11/2024 13:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name])
     VALUES
           (NEWID()
           ,'Administradores')
GO
 
 
INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name])
     VALUES
           (NEWID()
           ,'Estudiantes')
GO
INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name])
     VALUES
           (NEWID()
           ,'Profesores')
GO

select * from AspNetUsers
select * from AspNetUserRoles
select * from AspNetRoles

script 
-- Tabla: materias
CREATE TABLE materias (
    id_materia int IDENTITY(1,1) PRIMARY KEY,
    nombre varchar(100) NOT NULL,
    descripcion text
);

-- Tabla: tareas
CREATE TABLE tareas (
    id_tarea int IDENTITY(1,1) PRIMARY KEY,
    titulo varchar(150) NOT NULL,
    descripcion text,
    fecha_entrega datetime,
    id_materia int NOT NULL,
    archivo_adjunto varchar(255),

    CONSTRAINT FK_tareas_materias FOREIGN KEY (id_materia)
        REFERENCES materias(id_materia)
);

-- Tabla: entregas
CREATE TABLE entregas (
    id_entrega int IDENTITY(1,1) PRIMARY KEY,
    id_tarea int NOT NULL,
    id_estudiante nvarchar(128) NOT NULL,
    archivo_entregado varchar(255),
    fecha_entrega datetime,
    estado varchar(50),

    CONSTRAINT FK_entregas_tareas FOREIGN KEY (id_tarea)
        REFERENCES tareas(id_tarea),
    CONSTRAINT FK_entregas_usuarios FOREIGN KEY (id_estudiante)
        REFERENCES Usuarios_tb(IdUsuario)
);

-- Tabla: calificaciones
CREATE TABLE calificaciones (
    id_calificacion int IDENTITY(1,1) PRIMARY KEY,
    id_entrega int NOT NULL,
    calificacion decimal(5,2),
    comentario text,
    fecha_calificacion datetime,

    CONSTRAINT FK_calificaciones_entregas FOREIGN KEY (id_entrega)
        REFERENCES entregas(id_entrega)
);

CREATE TABLE grupos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_grupo INT NOT NULL,
    nombre_grupo VARCHAR(100) NOT NULL,
    descripcion TEXT,
    creado_por NVARCHAR(128) NOT NULL,
    id_usuario NVARCHAR(128) NOT NULL,
    estado VARCHAR(50)
);




-- Tabla: historial_calificaciones
CREATE TABLE historial_calificaciones (
    id_historial int IDENTITY(1,1) PRIMARY KEY,
    id_calificacion int NOT NULL,
    id_estudiante nvarchar(128) NOT NULL,
    id_materia int NOT NULL,
    fecha_registro datetime,
    comentario text,

    CONSTRAINT FK_historial_calificaciones_calificaciones FOREIGN KEY (id_calificacion)
        REFERENCES calificaciones(id_calificacion),
    CONSTRAINT FK_historial_calificaciones_usuarios FOREIGN KEY (id_estudiante)
        REFERENCES Usuarios_tb(IdUsuario),
    CONSTRAINT FK_historial_calificaciones_materias FOREIGN KEY (id_materia)
        REFERENCES materias(id_materia)
);

-- Tabla: anuncios
CREATE TABLE anuncios (
    id_anuncio int IDENTITY(1,1) PRIMARY KEY,
    titulo varchar(150),
    descripcion text,
    fecha_evento datetime,
    fecha_publicacion datetime
);




