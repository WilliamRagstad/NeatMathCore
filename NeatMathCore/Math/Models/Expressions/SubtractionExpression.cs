using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class SubtractionExpression : IExpression
    {
        public SubtractionExpression(IExpression term1, IExpression term2)
        {
            Term1 = term1;
            Term2 = term2;
        }

        public IExpression Term1;
        public IExpression Term2;

        public double Evaluate(Variables.VariableCollection variables)
        {
            return Term1.Evaluate(variables) - Term2.Evaluate(variables);
        }
        public string ToStringExpression() => Term1 + "-" + Term2;
    }
}
