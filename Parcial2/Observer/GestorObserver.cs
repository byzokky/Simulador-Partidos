using System;
using System.Collections.Generic;
using Parcial2.Torneo;
using System.Linq;

namespace Parcial2.Observer
{
    public class GestorObserver
    {
        #region Properties
        public List<IObserver> Suscriptores { get; set; }

        #endregion Properties

        #region Initialize
        public GestorObserver()
        {
            Suscriptores = new List<IObserver>();
        }
        #endregion Initialize


        #region Methods
        public void Suscribir(IObserver suscriber)
        {
            Suscriptores.Add(suscriber);
        }
        public void Desuscribir(IObserver suscriber)
        {
            Suscriptores.Remove(suscriber);
        }
        public void Notificar(Seleccion selec, int goles, int puntos)
        {
            try
            {
                IObserver sus = Suscriptores.First(s => s == selec);
                sus.Actualizar(goles, puntos);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Selección no suscrita: " + selec.Nombre);
            }
            
        }
        #endregion Methods
    }
}