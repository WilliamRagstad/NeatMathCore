using System;
using System.Collections.Generic;
using System.Globalization;
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

        public IExpression Evaluate(Variables.VariableCollection variables) => this;

        public string ToStringExpression() => Value.ToString(CultureInfo.InvariantCulture);
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
