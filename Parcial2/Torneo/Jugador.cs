using Newtonsoft.Json;

namespace Parcial2.Torneo
{
    public class Jugador
    {
        #region Properties  
        [JsonProperty("Nombre:")]
        public string Nombre { get; set; }

        [JsonProperty("Edad:")]
        public int Edad { get; set; }
        
        [JsonProperty("Posicion:")]
        public int Posicion { get; set; }

        [JsonProperty("Ataque:")]
        public double Ataque { get; set; }

        [JsonProperty("Defensa:")]
        public double Defensa { get; set; }

        [JsonProperty("Goles:")]
        public double Goles { get; set; }

        #endregion Properties

        #region Initialize

        public Jugador()
        {
            
        }

        #endregion Initialize

    }
}