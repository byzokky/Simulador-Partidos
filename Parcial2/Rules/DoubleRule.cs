using System;

namespace Parcial2.Rules
{
    public class DoubleRule : IRule
    {
        public bool Verificar(object value)
        {
            double result = 0;
            string valor = value as string;
            return Double.TryParse(valor, out result);
        }
    }
}