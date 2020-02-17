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

        public SubtractionExpression(IExpression term1, double term2) : this(term1, new ConstantExpression(term2)) { }
        public SubtractionExpression(double term1, IExpression term2) : this(new ConstantExpression(term1), term2) { }
        public SubtractionExpression(double term1, double term2) : this(new ConstantExpression(term1), new ConstantExpression(term2)) { }

        public IExpression Term1;
        public IExpression Term2;

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            return Term1.Evaluate(variables) - Term2.Evaluate(variables);
        }
        public string ToStringExpression() => Term1 + "-" + Term2;
        public override string ToString() => ToStringExpression();
    }
}
