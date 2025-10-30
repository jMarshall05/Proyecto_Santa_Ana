using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.editarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;

namespace Campus.AccesoDatos.Tareas.EditarTareaAD
{
    public class EditarTareaAD : IEditarTarea
    {
        private readonly Contexto _elContexto;

        public EditarTareaAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EditarTarea(int id, TareaDto tarea)
        {
            var tareaExistente = await _elContexto.Tareas.FindAsync(id);
            if (tareaExistente == null)
                return 0;

            // Validar grupo si está especificado
            if (tarea.Id_grupo > 0)
            {
                var grupoExiste = await _elContexto.Grupos
                    .AnyAsync(g => g.id_grupo == tarea.Id_grupo && g.estado == true);

                if (!grupoExiste)
                {
                    throw new ArgumentException("El grupo especificado no existe o esta inactivo");
                }
            }

            // Actualizar campos
            tareaExistente.Titulo = tarea.Titulo;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.FechaEntrega = tarea.FechaEntrega;
            tareaExistente.ArchivoAdjunto = tarea.ArchivoAdjunto;
            tareaExistente.FechaModificacion = DateTime.Now;
            tareaExistente.FechaPublicacion = tarea.FechaPublicacion;
            tareaExistente.IdGrupo = tarea.Id_grupo ;

            _elContexto.Entry(tareaExistente).State = EntityState.Modified;
            return await _elContexto.SaveChangesAsync();
        }
    }
}
