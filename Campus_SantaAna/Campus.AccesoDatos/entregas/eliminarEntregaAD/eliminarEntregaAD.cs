using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.entregas.eliminarEntregaAD;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;

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

            _elContexto.Entregas.Remove(entregaExistente);
            int resultado = await _elContexto.SaveChangesAsync();

            return resultado; // filas afectadas
        }
    }
}
