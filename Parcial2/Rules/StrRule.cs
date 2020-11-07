namespace Parcial2.Rules
{
	public class StrRule : IRule
	{
		public bool Verificar(object value)
		{
			return value is string;
		}
	}
}