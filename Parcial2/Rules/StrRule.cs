namespace Parcial2.Rules
{
	public class StrRule : IRule
	{
		public bool CheckRule(object value)
		{
			return value is string;
		}
	}
}