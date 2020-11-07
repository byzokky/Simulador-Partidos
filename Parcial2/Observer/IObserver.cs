using Parcial2.Torneo;

namespace Parcial2.Observer
{
    public interface IObserver
    {
        void Actualizar(int goles, int puntos);
    }
}