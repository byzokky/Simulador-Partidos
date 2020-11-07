using System.Collections.Generic;
using Newtonsoft.Json;
using Parcial2.Observer;
using Parcial2.Manejadores;
using System.Linq;

namespace Parcial2.Torneo
{
    public class Seleccion : IObserver
    {
        #region Properties  
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("puntos")]
        public int PuntosTotales { get; set; }

        [JsonProperty("goles")]
        public int GolesTotales { get; set; }

        [JsonProperty("jugadores")]
        public List<Jugador> Jugadores { get; set; }

        #endregion Properties

        #region Initialize

        public Seleccion()
        {
            
        }
        public Seleccion(string n, List<Jugador> j)
        {
            Nombre = n;
            PuntosTotales = 0;
            GolesTotales = 0;
            Jugadores = j.ToList();
        }

        #endregion Initialize
        
        #region Methods
        public void Actualizar(int goles, int puntos)
        {
            GolesTotales += goles;
            PuntosTotales += puntos;
        }

        #endregion Methods
    }
}