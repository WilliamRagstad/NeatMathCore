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

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            return Factor1.Evaluate(variables) * Factor2.Evaluate(variables);
        }

        public string ToStringExpression() {
            if (Factor1 is VariableExpression || Factor2 is VariableExpression) return Factor1.ToStringExpression() + Factor2.ToStringExpression();
            return Factor1.ToStringExpression() + "*" + Factor2.ToStringExpression();
        }
        public override string ToString() => ToStringExpression();
    }
}
