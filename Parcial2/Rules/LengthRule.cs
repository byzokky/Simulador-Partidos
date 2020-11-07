namespace Parcial2.Rules
{
	public class LengthRule : IRule
	{
		public bool Verificar(object value)
		{
			string strValue = value as string;
			return strValue.Length > 1;
		}
	}
}