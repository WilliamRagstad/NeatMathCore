using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class VariableExpression : IExpression
    {
        public VariableExpression(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }

        public double Evaluate(Variables.VariableCollection variables)
        {
            if (variables.Contains(Identifier))
            {
                Variables.Variable v = variables.Get(Identifier);
                if (v.Value.HasValue)
                    return v.Value.Value;
                else
                    throw new NotFiniteNumberException("Variable " + Identifier + " has no value");
            }
            else throw new NotFiniteNumberException("Variable " + Identifier + " is not defined");
        }

        public string ToStringExpression() => Identifier.ToString();
    }
}
