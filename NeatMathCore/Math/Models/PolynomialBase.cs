using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models
{
    abstract class PolynomialBase : IExpression
    {
        public string ToStringExpression(bool factorize, bool designation)
        {
            string result = "";

            if (Terms == null) return result;

            if (designation && !string.IsNullOrEmpty(Designation))
            {
                result += Designation + "(x)=";
            }
            if (factorize)
            {
                if (Factors.Length == 0) Factorize();
                for (int i = 0; i < Factors.Length; i++) result += Factors.ToString();
                return result;
            }
            else
            {
                for (int i = 0; i < Terms.Length; i++)
                {
                    if (Terms[i].Coefficient < 0 && i == 0)
                        result += "-";

                    if (i > 0)
                        result += Terms[i].Coefficient < 0 ? "-" : "+";
                    if (Terms[i].Exponent == 0)
                        result += "1";
                    else
                        result += System.Math.Abs(Terms[i].Coefficient) + Terms[i].Variable.Identifier + (Terms[i].Exponent == 1 ? "" : "^" + Terms[i].Exponent);
                }
                if (Constant != 0)
                {
                    if (Constant < 0)
                        result += "-" + -Constant;
                    else if (Terms.Length > 0)
                        result += "+" + Constant;
                    else result += Constant;
                }
                return result;
            }
        }

        public string ToStringExpression() => ToStringExpression(false, false);
        public override string ToString() => ToStringExpression(false, true);

        public string Designation;
        public PolynomialTerm[] Terms;
        public double Constant = 0;
        public int Degree { get
            {
                if (Terms == null) return 0;
                return Terms.Length;
            } }

        Expressions.EnclosedExpression[] Factors;
        
        public void Factorize()
        {
            Factors = new Expressions.EnclosedExpression[Terms.Length];
            PolynomialBase residualPolynomial = this;

            for (int i = 0; i < Terms.Length; i++)
            {
                double root = NewtonRaphsonMethod(residualPolynomial);
                IExpression factorExpression;
                if (root >= 0)
                    factorExpression = new Expressions.AdditionExpression(
                    Terms[i].Variable,
                    new Expressions.ConstantExpression(root)
                    );
                else
                    factorExpression = new Expressions.SubtractionExpression(
                        Terms[i].Variable,
                        new Expressions.ConstantExpression(root * -1)
                        );
                Factors[i] = new Expressions.EnclosedExpression(factorExpression);


                // Divide polynomial with root
                Classes.Polynomial rootPolynomial = new Classes.Polynomial(new PolynomialTerm[] { new PolynomialTerm(1, Terms[i].Variable, 1) }, root);
                residualPolynomial = residualPolynomial /  rootPolynomial;
            }
        }



        public PolynomialBase Derivative()
        {
            if (Degree < 1) return new Classes.Polynomial(0);
            Classes.Polynomial derivative = new Classes.Polynomial(Degree - 1);
            if (!string.IsNullOrEmpty(Designation)) derivative.Designation = Designation + "'";

            for (int i = 0; i < Terms.Length - 1; i++)
            {
                double newCoefficient = Terms[i].Coefficient * Terms[i].Exponent;
                int newExponent = Terms[i].Exponent - 1;
                derivative.Terms[i] = new PolynomialTerm(newCoefficient, Terms[i].Variable, newExponent);
            }
            if (Terms.Length - 1 >= 0)
            {
                derivative.Constant = Terms[Terms.Length - 1].Coefficient;
            }

            return derivative;
        }

        // Newton's method
        public static double NewtonRaphsonMethod(PolynomialBase polynomial, double x0 = 0)
        {
            double xn = x0;
            double threshold = 0.0001d;

            while(xn - x0 > threshold)
            {

            }
                
            return xn;
        }

        public double Evaluate(Variables.VariableCollection variables)
        {
            throw new NotImplementedException();
        }

        public static PolynomialBase operator /(PolynomialBase dividend, PolynomialBase divisor)
        {
            throw new NotImplementedException();
        }
        public static PolynomialBase operator -(PolynomialBase dividend, PolynomialBase divisor)
        {
            throw new NotImplementedException();
        }
        public static PolynomialBase operator +(PolynomialBase dividend, PolynomialBase divisor)
        {
            throw new NotImplementedException();
        }
    }
}
