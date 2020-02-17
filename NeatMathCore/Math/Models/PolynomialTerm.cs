using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models
{
    class PolynomialTerm : IExpression
    {
        public PolynomialTerm(double coefficient, Expressions.VariableExpression variable, int exponent)
        {
            Coefficient = coefficient;
            Variable = variable;
            Exponent = exponent;
        }

        public double Coefficient { get; }
        public Expressions.VariableExpression Variable { get; }
        public int Exponent { get; }

        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            IExpression variableValue = Variable.Evaluate(variables);
            if (variableValue is Expressions.ConstantExpression)
                return new Expressions.ConstantExpression(Coefficient * System.Math.Pow(((Expressions.ConstantExpression)variableValue).Value, Exponent));
            else
                return this;
        }

        public string ToStringExpression()
        {
            if (Coefficient == 0)
                return "0";
            if (Coefficient == 1)
                return $"{Variable}^{Exponent}";
            return $"{Coefficient}*{Variable}^{Exponent}";
        }
        public string ToStringExpression(bool coefficientSign)
        {
            if (!coefficientSign) return $"{System.Math.Abs(Coefficient)}*{Variable}^{Exponent}";
            return ToStringExpression();
        }
    }
}
