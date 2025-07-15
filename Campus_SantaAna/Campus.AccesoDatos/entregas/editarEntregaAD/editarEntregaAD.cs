using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.entregas.editarEntregaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;

namespace Campus.AccesoDatos.Entregas.EditarEntregaAD
{
    public class EditarEntregaAD : IEditarEntrega
    {
        private readonly Contexto _elContexto;

        public EditarEntregaAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EditarEntrega(EntregasDto entrega)
        {
            var entregaExistente = await _elContexto.Entregas
                .FindAsync(entrega.id_entrega);

            if (entregaExistente == null)
            {
                throw new ArgumentException("La entrega especificada no existe");
            }

            // Actualizar campos
            entregaExistente.IdTarea = entrega.id_tarea;
            entregaExistente.IdEstudiante = entrega.id_estudiante;
            entregaExistente.ArchivoEntregado = entrega.archivo_entregado;
            entregaExistente.FechaEntrega = entrega.fecha_entrega < new DateTime(1753, 1, 1)
                ? DateTime.Now
                : entrega.fecha_entrega;
            entregaExistente.Estado = entrega.estado;

            _elContexto.Entry(entregaExistente).State = EntityState.Modified;
            int resultado = await _elContexto.SaveChangesAsync();

            return resultado; // filas afectadas
        }
    }
}
