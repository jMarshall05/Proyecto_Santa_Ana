using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.entregas.agregarEntregaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Entregas.AgregarEntregaAD
{
    public class AgregarEntregaAD : IAgregarEntrega
    {
        private readonly Contexto _elContexto;

        public AgregarEntregaAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarEntrega(EntregasDto entrega)
        {
            // Validar que la tarea exista
            var tareaExiste = await _elContexto.Tareas
                .AnyAsync(t => t.IdTarea == entrega.id_tarea);
            if (!tareaExiste)
            {
                throw new ArgumentException("La tarea especificada no existe");
            }

            // Validar que el usuario exista (id_estudiante es string, usuario es Usuarios)
            var usuarioExiste = await _elContexto.Usuarios
                .AnyAsync(u => u.IdUsuario == entrega.id_estudiante);
            if (!usuarioExiste)
            {
                throw new ArgumentException("El usuario especificado no existe");
            }

            var entregaTransformada = ConvertirAD(entrega);

            _elContexto.Entregas.Add(entregaTransformada);
            _elContexto.Entry(entregaTransformada).State = EntityState.Added;

            await _elContexto.SaveChangesAsync();

            return entregaTransformada.IdEntrega;
        }

        private EntregasAD ConvertirAD(EntregasDto entrega)
        {
            return new EntregasAD
            {
                IdTarea = entrega.id_tarea,
                IdEstudiante = entrega.id_estudiante,
                ArchivoEntregado = entrega.archivo_entregado,
                FechaEntrega = entrega.fecha_entrega < new DateTime(1753, 1, 1) ? DateTime.Now : entrega.fecha_entrega,
                Estado = entrega.estado
            };
        }
    }
}
