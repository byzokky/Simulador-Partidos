using Newtonsoft.Json;

namespace Parcial2.Torneo
{
    public class Jugador
    {
        #region Properties  
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("edad")]
        public int Edad { get; set; }
        
        [JsonProperty("posicion")]
        public int Posicion { get; set; }

        [JsonProperty("ataque")]
        public double Ataque { get; set; }

        [JsonProperty("defensa")]
        public double Defensa { get; set; }

        [JsonProperty("goles")]
        public double Goles { get; set; }

        #endregion Properties

        #region Initialize

        public Jugador()
        {
            
        }

        #endregion Initialize

    }
}