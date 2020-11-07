using System.Collections.Generic;
using Parcial2.Rules;

namespace Parcial2.Validators
{
    public class Validator
    {
        public List<IRule> RuleList { get; set; }

        public Validator()
        {
            RuleList = new List<IRule>();
        }

        public bool ValidateField(object Value)
        {
            bool response = true;
            RuleList.ForEach(r =>
            {
                bool result = r.CheckRule(Value);
                response = response && result;
            });
            return response;
        }
    }
}