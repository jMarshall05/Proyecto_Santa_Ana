// Archivo: Campus.Abstracciones.ModelosUI/EstudianteGrupoDto.cs

using System;

namespace Campus.Abstracciones.ModelosUI
{
    public class EstudianteGrupoDto
    {
        public int IdEstudianteGrupo { get; set; }
        public string EstudianteId { get; set; }
        public int GrupoId { get; set; }

        // Añadido para mostrar nombre del grupo en la vista
        public string NombreGrupo { get; set; }
    }
}
