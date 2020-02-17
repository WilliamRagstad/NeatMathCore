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

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            if (variables.Contains(Identifier))
            {
                Variables.Variable v = variables.Get(Identifier);
                if (v.Value.HasValue)
                    return new ConstantExpression(v.Value.Value);
                else
                    return this;
            }
            else return this;
        }

        public string ToStringExpression() => Identifier.ToString();
        public override string ToString() => ToStringExpression();
    }
}
