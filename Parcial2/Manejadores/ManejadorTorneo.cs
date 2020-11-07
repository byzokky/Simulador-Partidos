using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Parcial2.Observer;
using Parcial2.Rules;
using Parcial2.Torneo;
using Parcial2.Validators;

namespace Parcial2.Manejadores
{
    public class ManejadorTorneo
    {
        public ManejadorJson JsonHandler { get; set; }
        public Validator StringValidate { get; set; }
        public Validator IntValidate { get; set; }
        public Validator DoubleValidate { get; set; }

        public ManejadorTorneo()
        {
            JsonHandler = new ManejadorJson();

            StringValidate = new Validator();
            StringValidate.RuleList.Add(new StrRule());
            StringValidate.RuleList.Add(new LengthRule());

            IntValidate = new Validator();
            IntValidate.RuleList.Add(new IntRule());

            DoubleValidate = new Validator();
            DoubleValidate.RuleList.Add(new DoubleRule());
        }

        public void CargarSelecciones(List<Seleccion> selecciones)
        {
            Seleccion s;
            string path = @".\";
            string searchPattern = "*.json";
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files =
            di.GetFiles(searchPattern);
            foreach (FileInfo file in files)
            {
                s = JsonHandler.Load(file.Name.Remove(file.Name.Length - 5));
                selecciones.Add(s);
            }
        }
        public int MostrarSelecciones(List<Seleccion> selecciones)
        {
            int amount = 0;
            Console.WriteLine("Selecciones existentes: ");
            selecciones.ForEach(s => {
                Console.WriteLine(s.Nombre);
                amount++;
            });
            return amount;
        }

        

        public void CrearJugador(Jugador j, List<string> atributos)
        {
            j.Nombre = atributos[0];
            j.Edad = Convert.ToInt32(atributos[1]);
            j.Posicion = Convert.ToInt32(atributos[2]);
            j.Ataque = Convert.ToDouble(atributos[3]);
            j.Defensa = Convert.ToDouble(atributos[4]);
            j.Goles = 0;
        }

        public void AgregarJugador(List<Seleccion> selecciones)
        {
            Seleccion s;
            try
            {
                int amount = MostrarSelecciones(selecciones);
                if(amount > 0)
                {
                    Console.Write("Ingrese el nombre de la selección: ");
                    string nombreSelec = Console.ReadLine();
                    s = selecciones.First(p => p.Nombre == nombreSelec);
                    bool validate = true;
                    string value;
                    List<string> atributos = new List<string>();
                    if(s.Jugadores.Count <= 22)
                    {
                        Console.WriteLine("Ingrese la información del futbolista");
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
                            s.Jugadores.Add(new Jugador());
                            CrearJugador(s.Jugadores.Last(), atributos);
                            JsonHandler.Save(s);
                        }
                        else
                        {
                            Console.WriteLine("Se ingresaron datos invalidos");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Selección llena");
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

        public void MostrarJugadores(Seleccion s)
        {
            s.Jugadores.ForEach(j => 
            {
                Console.Write($"{j.Nombre} | Edad:{j.Edad} | Posición:{j.Posicion} | ");
                Console.WriteLine($"Ataque:{j.Ataque} | Defensa:{j.Defensa}");
            });
        }

        public void EditarJugador(List<Seleccion> selecciones)
        {
            string nombre;
            try
            {
                if(MostrarSelecciones(selecciones) > 0)
                {
                    Console.WriteLine("Ingrese el nombre de la selección: ");
                    string nombreSelec = Console.ReadLine();
                    Seleccion s = selecciones.First(p => nombreSelec == p.Nombre);
                    MostrarJugadores(s);
                    bool validate;
                    Console.Write("Ingrese el nombre del jugador: ");
                    nombre = Console.ReadLine();
                    validate = StringValidate.ValidateField(nombre);
                    if(validate)
                    {
                        Jugador j = s.Jugadores.First(j => j.Nombre == nombre);
                        string value;
                        List<string> atributos = new List<string>();
                        atributos.Add(j.Nombre);
                        Console.Write("Edad:");
                        value = Console.ReadLine();
                        atributos.Add(value);
                        validate = validate && IntValidate.ValidateField(value);
                        Console.Write("Posición:");
                        value = Console.ReadLine();
                        atributos.Add(value);
                        validate = validate && IntValidate.ValidateField(value);
                        Console.Write("Ataque:");
                        value = Console.ReadLine();
                        atributos.Add(value);
                        validate = validate && DoubleValidate.ValidateField(value);
                        Console.Write("Defensa:");
                        value = Console.ReadLine();
                        atributos.Add(value);
                        validate = validate && DoubleValidate.ValidateField(value);
                        if(validate)
                        {
                            CrearJugador(j, atributos);
                            JsonHandler.Save(s);
                        }
                        else
                        {
                            Console.WriteLine("Se ingresaron datos invalidos");
                        }
                    }
                        
                }
                else
                {
                    Console.WriteLine("No hay selecciones");
                }
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador u selección");
            }

        }

        public void EjecutarPartido(Partido PartidoActual, Seleccion Local, Seleccion Visitante)
        {
            string result = PartidoActual.Resultado();
            Console.WriteLine("Resultado: " + result);
            int golesLocal = PartidoActual.EquipoLocal.Goles;
            int golesVisitante = PartidoActual.EquipoVisitante.Goles;
            int puntosLocal = 0;
            int puntosVisitante = 0;
            if(golesLocal > golesVisitante)
            {
                puntosLocal = 3;
            }
            else if(golesLocal == golesVisitante)
            {
                puntosLocal = 1;
                puntosVisitante = 1;
            }
            else
            {
                puntosVisitante = 3;
            }
            
            JsonHandler.Save(Local);
            JsonHandler.Save(Visitante);
        }
    }
}