using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class AdditionExpression : IExpression
    {
        public AdditionExpression(IExpression term1, IExpression term2)
        {
            Term1 = term1;
            Term2 = term2;
        }
        public AdditionExpression(IExpression term1, double term2) : this(term1, new ConstantExpression(term2)) { }
        public AdditionExpression(double term1, IExpression term2) : this(new ConstantExpression(term1), term2) { }
        public AdditionExpression(double term1, double term2) : this(new ConstantExpression(term1), new ConstantExpression(term2)) { }

        public IExpression Term1;
        public IExpression Term2;

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            return Term1.Evaluate(variables) + Term2.Evaluate(variables);
        }

        public string ToStringExpression() => Term1.ToStringExpression() + "+" + Term2.ToStringExpression();
        public override string ToString() => ToStringExpression();
    }
}
