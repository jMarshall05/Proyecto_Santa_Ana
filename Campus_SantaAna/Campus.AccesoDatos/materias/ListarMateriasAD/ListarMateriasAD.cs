using System.Collections.Generic;
using System.Linq;
using Campus.Abstracciones.AccesoDatos.Materias.ListarMateriasAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Materias.ListarMateriasAD
{
    public class ListarMateriasAD : IListarMateriasAD
    {
        private readonly Contexto _elContexto;

        public ListarMateriasAD()
        {
            _elContexto = new Contexto();
        }

        public List<MateriaDto> ListarMaterias()
        {
            return (from materia in _elContexto.Materias
                    select new MateriaDto
                    {
                        Id_Materia = materia.IdMateria,
                        Nombre = materia.Nombre
                    }).ToList();
        }

        public MateriaDto ObtenerMateriaPorId(int id)
        {
            var materia = _elContexto.Materias.FirstOrDefault(m => m.IdMateria == id);

            if (materia == null) return null;

            return new MateriaDto
            {
                Id_Materia = materia.IdMateria,
                Nombre = materia.Nombre
            };
        }
    }
}
