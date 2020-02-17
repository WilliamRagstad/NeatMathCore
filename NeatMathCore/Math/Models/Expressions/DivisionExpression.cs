using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class DivisionExpression : IExpression
    {
        public DivisionExpression(IExpression numerator, IExpression denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public IExpression Numerator { get; }
        public IExpression Denominator { get; }

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            return Numerator.Evaluate(variables) / Denominator.Evaluate(variables);
        }

        public string ToStringExpression() => Numerator + "/" + Denominator;
        public override string ToString() => ToStringExpression();
    }
}
