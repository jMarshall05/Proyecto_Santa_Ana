using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.entregas.eliminarEntregaAD;

namespace Campus.AccesoDatos.Entregas.EliminarEntregaAD
{
    public class EliminarEntregaAD : IEliminarEntrega
    {
        private readonly Contexto _elContexto;

        public EliminarEntregaAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EliminarEntrega(int id_entrega)
        {
            var entregaExistente = await _elContexto.Entregas.FindAsync(id_entrega);

            if (entregaExistente == null)
            {
                throw new ArgumentException("La entrega especificada no existe");
            }

            entregaExistente.Estado = false;
            _elContexto.Entry(entregaExistente).State = EntityState.Modified;
            int resultado = await _elContexto.SaveChangesAsync();

            return resultado; // filas afectadas
        }
    }
}
