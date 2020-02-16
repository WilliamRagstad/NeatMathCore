using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class EnclosedExpression : IExpression
    {
        public EnclosedExpression(IExpression expression)
        {
            Expression = expression;
        }
        
        public IExpression Expression { get; }

        public double Evaluate(Variables.VariableCollection variables)
        {
            return Expression.Evaluate(variables);
        }

        public string ToStringExpression()
        {
            if (Expression is ConstantExpression || Expression is VariableExpression) return Expression.ToStringExpression();
            return "(" + Expression.ToStringExpression() + ")";
        }
    }
}
