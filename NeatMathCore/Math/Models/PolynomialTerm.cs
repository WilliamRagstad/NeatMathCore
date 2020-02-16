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

        public double Evaluate(Variables.VariableCollection variables)
        {
            return Coefficient * System.Math.Pow(Variable.Evaluate(variables), Exponent);
        }

        public string ToStringExpression() => $"{Coefficient}*{Variable}^{Exponent}";
        public string ToStringExpression(bool coefficientSign)
        {
            if (!coefficientSign) return $"{System.Math.Abs(Coefficient)}*{Variable}^{Exponent}";
            return ToStringExpression();
        }
    }
}
