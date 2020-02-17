using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class NullExpression : IExpression
    {
        public NullExpression() {}

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            return null;
        }

        public string ToStringExpression() => "[NULL_EXPR]";
        public override string ToString() => ToStringExpression();
    }
}
