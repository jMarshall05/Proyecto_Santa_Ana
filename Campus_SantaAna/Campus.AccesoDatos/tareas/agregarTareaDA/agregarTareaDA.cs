using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.agregarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;
using System;

namespace Campus.AccesoDatos.Tareas.AgregarTareaAD
{
    public class AgregarTareaAD : IAgregarTarea
    {
        private readonly Contexto _elContexto;

        public AgregarTareaAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarTarea(TareaDto tarea)
        {
            // Validar que el grupo exista si está especificado
            if (tarea.Id_grupo > 0)
            {
                var grupoExiste = await _elContexto.Grupos
                    .AnyAsync(g => g.id_grupo == tarea.Id_grupo);

                if (!grupoExiste)
                {
                    throw new ArgumentException("El grupo especificado no existe");
                }
            }

            var tareaTransformada = ConvertirAD(tarea);
            _elContexto.Tareas.Add(tareaTransformada);
            _elContexto.Entry(tareaTransformada).State = System.Data.Entity.EntityState.Added;
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }

        private TareasAD ConvertirAD(TareaDto tarea)
        {
            return new TareasAD
            {
                Titulo = tarea.Titulo,
                id_materia = tarea.IdMateria,
                Descripcion = tarea.Descripcion,
                ArchivoAdjunto = tarea.ArchivoAdjunto,
                FechaEntrega = tarea.FechaEntrega < new DateTime(1753, 1, 1) ? DateTime.Now.AddDays(1) : tarea.FechaEntrega,
                FechaPublicacion = tarea.FechaPublicacion < new DateTime(1753, 1, 1) ? DateTime.Now : tarea.FechaPublicacion,
                IdGrupo = tarea.Id_grupo,
                asignado_por = tarea.asignado_por,
                Estado = tarea.Estado
            };
        }



    }
}
