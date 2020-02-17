using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Expressions
{
    class ExponentExpression : IExpression
    {
        public ExponentExpression(IExpression baseExpression, IExpression exponentExpression)
        {
            Base = baseExpression;
            Exponent = exponentExpression;

            if (!(Exponent is ConstantExpression || Exponent is VariableExpression || Exponent is ExponentExpression))
            {
                //   6 + x
                //  e       =>   e^(6+x)

                Exponent = new EnclosedExpression(Exponent);
            }
        }

        public IExpression Base;
        public IExpression Exponent;

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            IExpression expExpr = Exponent.Evaluate(variables);
            if (expExpr is ConstantExpression && ((ConstantExpression)expExpr).Value == 0) return new ConstantExpression(1);
            IExpression baseExpr = Base.Evaluate(variables);
            if (expExpr is ConstantExpression && ((ConstantExpression)expExpr).Value == 1) return baseExpr;
            return this;
        }

        public string ToStringExpression() => Base + "^" + Exponent;
        public override string ToString() => ToStringExpression();
    }
}
