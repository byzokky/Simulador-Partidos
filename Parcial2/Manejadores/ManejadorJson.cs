using System.IO;
using Newtonsoft.Json;
using Parcial2.Torneo;

namespace Parcial2.Manejadores
{
    
    public class ManejadorJson
    {
        public void Save(Seleccion s)
        {
            string filename = "./" + s.Nombre + ".json";
            var seleccionSerializada = JsonConvert.SerializeObject(s);
            File.WriteAllText(filename, seleccionSerializada);
        }

        public Seleccion Load(string name)
        {
            try
            {
                string filename = "./" + name + ".json";
                Seleccion s = JsonConvert.DeserializeObject<Seleccion>(File.ReadAllText(filename));
                return s;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException(name);
            }
            
        }
    }
}