using System.Threading;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.agregarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

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
                Descripcion = tarea.Descripcion,
                ArchivoAdjunto = tarea.ArchivoAdjunto,
                FechaEntrega = tarea.FechaEntrega,
                FechaCreacion = tarea.FechaCreacion,
                FechaPublicacion = tarea.FechaPublicacion

            };
        }
    }
}
