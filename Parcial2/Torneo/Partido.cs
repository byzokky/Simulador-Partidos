using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace Parcial2.Torneo
{
    public class Partido
    {
        #region Properties  
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }

        #endregion Properties

        #region Initialize
        
        public Partido()
        {

        }
        public Partido(Seleccion Local, Seleccion Visitante) 
        {
            EquipoLocal = new Equipo(Local, true);
            EquipoVisitante = new Equipo(Visitante, false);
        }
        #endregion Initialize
        #region Methods

        private List<String> GetNames()
        {
            List<String> nombres = new List<string>();
            Random random = new Random();
            List<string> jugadoresVacios = Enumerable.Repeat(String.Empty, 30).ToList();
            List<String> JugadoresLocales = EquipoLocal.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            int position = random.Next(JugadoresLocales.Count);
            nombres.Add(JugadoresLocales[position]);
            position = random.Next(JugadoresVisitantes.Count);
            nombres.Add(JugadoresVisitantes[position]);
            return nombres;
        }
        private void CalcularExpulsiones()
        {
            List<String> nombres = GetNames();
            EquipoLocal.ExpulsarJugador(nombres[0]);
            EquipoVisitante.ExpulsarJugador(nombres[1]);
        }

        private void CalcularAmarillas()
        {
            List<String> nombres = GetNames();
            EquipoLocal.DarAmarilla(nombres[0]);
            EquipoVisitante.DarAmarilla(nombres[1]);
        }

        private void CalcularResultado()
        {
            Random random = new Random();
            EquipoLocal.Goles = random.Next(0,6);
            EquipoVisitante.Goles = random.Next(0,6);
        }

        public string Resultado()
        {
            string resultado = "0 - 0";
            try
            {
                for(int i = 0; i < 8; i++)
                {
                    CalcularAmarillas();
                }

                for(int i = 0; i < 5; i++)
                {
                    CalcularExpulsiones();
                }
                
                CalcularResultado();
                resultado = EquipoLocal.Goles.ToString() + " - " + EquipoVisitante.Goles.ToString();
            }
            catch(LoseForWException ex)
            {
                Console.WriteLine(ex.Message);
                EquipoLocal.Goles -= EquipoLocal.Goles;
                EquipoVisitante.Goles -= EquipoVisitante.Goles;
                if (ex.NombreEquipo == EquipoLocal.Seleccion.Nombre)
                {
                    EquipoVisitante.Goles = 3;
                    resultado = "0 - 3";
                }
                else
                {
                    EquipoLocal.Goles = 3;
                    resultado = "3 - 0";
                }
            }
            
            return resultado;
        }
        #endregion Methods

    }
}