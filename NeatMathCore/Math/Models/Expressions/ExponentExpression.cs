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
                //  e          =  e^(6+x)

                Exponent = new EnclosedExpression(Exponent);
            }
        }

        public IExpression Base;
        public IExpression Exponent;

        public double Evaluate(Variables.VariableCollection variables)
        {
            double expVal = Exponent.Evaluate(variables);
            if (expVal == 0) return 1;
            double baseVal = Base.Evaluate(variables);
            if (expVal == 1) return baseVal;
            return System.Math.Pow(Base.Evaluate(variables), Exponent.Evaluate(variables));
        }

        public string ToStringExpression()
        {
            return Base + "^" + Exponent;
        }
    }
}
