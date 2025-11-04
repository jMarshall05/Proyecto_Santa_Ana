using Campus.Abstracciones.AccesoDatos.Bitacora;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Bitacora;

namespace Campus.LogicaDeNegocio.Bitacora
{
    public class BitacoraLN : IBitacoraLN
    {
        private readonly IBitacoraAD _bitacoraAD;
        public BitacoraLN()
        {
            _bitacoraAD = new BitacoraAD();
        }
        public void RegistrarEvento(BitacoraDto bitacora)
        {
            _bitacoraAD.RegistrarEvento(bitacora);
        }
    }
}
