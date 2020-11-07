using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Parcial2.Torneo;
using Parcial2.Manejadores;
using Parcial2.Validators;
using Parcial2.Rules;

namespace Parcial2.Menus
{
    public class Menu
    {
        public Partido PartidoActual { get; set; }
        public Seleccion Local { get; set; }
        public Seleccion Visitante { get; set; }
        public ManejadorTorneo Gestor { get; set; }
        public List<Seleccion> Selecciones { get; set; }
        public Validator StringValidate { get; set; }
        public Validator IntValidate { get; set; }
        public Validator DoubleValidate { get; set; }
        
        public Menu()
        {
            PartidoActual = new Partido();
            Gestor = new ManejadorTorneo();
            Selecciones = new List<Seleccion>();
            StringValidate = new Validator();
            StringValidate.RuleList.Add(new StrRule());
            StringValidate.RuleList.Add(new LengthRule());

            IntValidate = new Validator();
            IntValidate.RuleList.Add(new IntRule());

            DoubleValidate = new Validator();
            DoubleValidate.RuleList.Add(new DoubleRule());
        }

        public void TheMenu()
        {
            Gestor.CargarSelecciones(Selecciones);
            string opcion = "";
            Console.WriteLine("Bienvenido al sistema de partidos");
            do
            {
                Console.WriteLine("Seleccione una opción: ");
                Console.WriteLine("1.Crear partido");
                Console.WriteLine("2.Crear selección");
                Console.WriteLine("3.Agregar jugador a selección");
                Console.WriteLine("4.Editar jugador de una selección");
                Console.WriteLine("5.Realizar partido");
                Console.WriteLine("0.Salir");
                Console.Write("> ");
                opcion = Console.ReadLine();
                switch(opcion)
                {
                    case "0":
                        break;
                    
                    case "1":
                        CrearPartido();
                        break;
                    
                    case "2":
                        CrearSeleccion();
                        break;
                    
                    case "3":
                        Gestor.AgregarJugador(Selecciones);
                        break;

                    case "4":
                        Gestor.EditarJugador(Selecciones);
                        break;
                    
                    case "5":
                        Gestor.EjecutarPartido(PartidoActual, Local, Visitante);
                        break;
                    default:
                        Console.WriteLine("Opción Incorrecta");
                        break;
                }
            } while(opcion != "0");
        }
        public void CrearPartido()
        {
            try
            {
                int amount = Gestor.MostrarSelecciones(Selecciones);
                if(amount > 1)
                {
                    Console.WriteLine("Ingrese el nombre de la selección local: ");
                    string nombreLocal = Console.ReadLine();
                    Console.WriteLine("Ingrese el nombre de la selección visitante: ");
                    string nombreVisitante = Console.ReadLine();
                    if(nombreLocal != nombreVisitante)
                    {
                        Local = Selecciones.First(s => nombreLocal == s.Nombre);
                        Visitante = Selecciones.First(s => nombreVisitante == s.Nombre);
                        PartidoActual = new Partido(Local, Visitante);
                    }
                }
                else
                {
                    Console.WriteLine("No hay suficientes selecciones");
                }   
            }
            catch (InvalidOperationException) 
            {
                Console.WriteLine("Selección no encontrada");
            }
        }

        public void CrearSeleccion()
        {
            bool validate;
            Console.Write("Ingrese el nombre de la selección: ");
            string nombreSelec = Console.ReadLine();
            validate = StringValidate.ValidateField(nombreSelec);
            int cantJugadores = 0;
            bool stop = false;
            if(validate)
            {
                List<Jugador> listaJugadores = new List<Jugador>();
                string value;
                List<string> atributos = new List<string>();
                while(!stop && cantJugadores < 23)
                {
                    atributos.Clear();
                    Console.WriteLine($"Jugador #{cantJugadores + 1}");
                    Console.Write("Nombre: ");
                    value = Console.ReadLine();
                    atributos.Add(value);
                    validate = StringValidate.ValidateField(value);
                    Console.Write("Edad: ");
                    value = Console.ReadLine();
                    atributos.Add(value);
                    validate = validate && IntValidate.ValidateField(value);
                    Console.Write("Posición: ");
                    value = Console.ReadLine();
                    atributos.Add(value);
                    validate = validate && IntValidate.ValidateField(value);
                    Console.Write("Ataque: ");
                    value = Console.ReadLine();
                    atributos.Add(value);
                    validate = validate && DoubleValidate.ValidateField(value);
                    Console.Write("Defensa: ");
                    value = Console.ReadLine();
                    atributos.Add(value);
                    validate = validate && DoubleValidate.ValidateField(value);
                    if(validate)
                    {
                        listaJugadores.Add(new Jugador());
                        cantJugadores++;
                    }
                    else
                    {
                        Console.WriteLine("Se ingresaron datos invalidos");
                    }
                    if(cantJugadores >= 11 && cantJugadores < 23)
                    {
                        Console.WriteLine("Se ha alcanzado el mínimo requerido");
                        Console.WriteLine("Ingrese '1' para parar, cualquier otra cosa para continuar");
                        if(Console.ReadLine() == "1")
                        {
                            stop = true;
                        }
                    }
                    if(cantJugadores == 23)
                    {
                        Console.WriteLine("Se ha alcanzado el máximo de jugadores");
                    } 
                }
                Seleccion s = new Seleccion(nombreSelec, listaJugadores);
            }
            else
            {
                Console.WriteLine("Nombre invalido");
            }
        }
    }
}