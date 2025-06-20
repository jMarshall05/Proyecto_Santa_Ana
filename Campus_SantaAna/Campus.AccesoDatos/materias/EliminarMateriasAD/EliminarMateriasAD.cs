using System;
using System.Linq;
using Campus.Abstracciones.AccesoDatos.Materias.EliminarMateriasAD;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Materias.EliminarMateriasAD
{
    public class EliminarMateriasAD : IEliminarMateriasAD
    {
        private Contexto _elContexto;

        public EliminarMateriasAD()
        {
            _elContexto = new Contexto();
        }

        public void EliminarMateria(int materiaId)
        {
            var materiaAEliminar = _elContexto.Materias.FirstOrDefault(m => m.IdMateria == materiaId);
            if (materiaAEliminar != null)
            {
                _elContexto.Materias.Remove(materiaAEliminar);
                _elContexto.SaveChanges();
            }
            else
            {
                throw new Exception("La materia no fue encontrada.");
            }
        }
    }
}
