using System;

namespace Parcial2.Rules
{
    public class IntRule : IRule
    {
        public bool Verificar(object value)
        {
            int result = 0;
            string valor = value as string;
            return Int32.TryParse(valor, out result);
        }
    }
}