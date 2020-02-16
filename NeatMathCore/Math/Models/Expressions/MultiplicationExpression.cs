using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class MultiplicationExpression : IExpression
    {
        public MultiplicationExpression(IExpression factor1, IExpression factor2)
        {
            Factor1 = factor1;
            Factor2 = factor2;
        }

        public IExpression Factor1;
        public IExpression Factor2;

        public double Evaluate(Variables.VariableCollection variables)
        {
            return Factor1.Evaluate(variables) * Factor2.Evaluate(variables);
        }

        public string ToStringExpression() => Factor1 + "*" + Factor2;
    }
}
