using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class ConstantExpression : IExpression
    {
        public ConstantExpression(double value)
        {
            Value = value;
        }

        public double Value;

        public double Evaluate(Variables.VariableCollection variables)
        {
            return Value;
        }

        public string ToStringExpression() => Value.ToString();
    }
}
