using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace Parcial2.Torneo
{
    public class Equipo
    {
        #region Properties  
        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
        public Seleccion Seleccion { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public List<Jugador> JugadoresAmarilla { get; set; }
        public bool EsLocal { get; set; }

        #endregion Properties

        #region Initialize
        public Equipo(Seleccion s, bool local)
        {
            Seleccion = s;
            // Se le agregó .ToList para que creara una copia de la lista y no se pasara como referencia
            Jugadores = Seleccion.Jugadores.ToList();
            JugadoresAmarilla = new List<Jugador>();
            EsLocal = local;
        }
        #endregion Initialize

        #region Methods
        public void ExpulsarJugador(string name)
        {
            try
            {
                Jugador jugadorExpulsado = Jugadores.First(j => j.Nombre == name);
                TarjetasRojas++;
                Jugadores.Remove(jugadorExpulsado);
                if (TarjetasRojas >= 5)
                {
                    LoseForWException ex = new LoseForWException(Seleccion.Nombre);
                    ex.NombreEquipo = Seleccion.Nombre;
                    throw ex;
                }
                Console.WriteLine($"El jugador {jugadorExpulsado.Nombre} fue expulsado");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador para expulsarlo del equipo " + Seleccion.Nombre);
            }
            
        }

        public void DarAmarilla(string name)
        {
            try
            {
                if(JugadoresAmarilla.Any(j => j.Nombre == name))
                {
                    TarjetasAmarillas++;
                    Jugador expulsado = JugadoresAmarilla.First(j => j.Nombre == name);
                    Console.WriteLine($"El jugador {expulsado.Nombre} recibió una tarjeta amarilla");
                    ExpulsarJugador(expulsado.Nombre);
                    JugadoresAmarilla.Remove(expulsado);
                }
                else
                {
                    Jugador jugadorAmarilla = Jugadores.First(j => j.Nombre == name);
                    TarjetasAmarillas++;
                    JugadoresAmarilla.Add(jugadorAmarilla);
                    Console.WriteLine($"El jugador {jugadorAmarilla.Nombre} recibió una tarjeta amarilla");
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador para que reciba tarjeta amarilla del equipo " 
                                    + Seleccion.Nombre);
            }
        }
        #endregion Methods
    }
}