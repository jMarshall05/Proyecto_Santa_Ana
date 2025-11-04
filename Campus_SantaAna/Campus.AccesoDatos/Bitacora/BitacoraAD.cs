using System;
using Campus.Abstracciones.AccesoDatos.Bitacora;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Bitacora
{
    public class BitacoraAD : IBitacoraAD
    {
        private Contexto _elContexto;
        public BitacoraAD()
        {
            _elContexto = new Contexto();
        }

        public void RegistrarEvento(BitacoraDto bitacora)
        {
            var nuevoEvento = new ModelosAD.BitacoraAD
            {
                IdBitacora = Guid.NewGuid().ToString(),
                Fecha = bitacora.Fecha,
                usuario = bitacora.Usuario,
                accion = bitacora.Accion,
                Tabla = bitacora.Tabla,
                descripcion = bitacora.Descripcion
            };
            _elContexto.Bitacora.Add(nuevoEvento);
            var resultado = _elContexto.SaveChangesAsync();

        }
    }
}
