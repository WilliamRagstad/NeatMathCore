using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models
{
    abstract class PolynomialBase : IExpression
    {
        public string ToStringExpression(bool factorize, bool designation = false)
        {
            string result = "";

            if (Terms == null) return result;

            if (designation && !string.IsNullOrEmpty(Designation))
            {
                result += Designation + "("; // "(x)=";
                for (int i = 0; i < DependentVariables.Length; i++)
                {
                    result += DependentVariables[i];
                    if (i < DependentVariables.Length - 1)
                        result += ',';
                }
                result += ")=";
            }
            if (factorize)
            {
                if (Factors == null || Factors.Length == 0) Factorize();    // Maybe remove check and factorize every time?
                for (int i = 0; i < Factors.Length; i++)
                {
                    if (Factors[i].Expression is Expressions.ConstantExpression)
                    {
                        Expressions.ConstantExpression c = Factors[i].Expression as Expressions.ConstantExpression;
                        if (c.Value == 0)
                            return "0";
                        else if (c.Value == 1)
                            continue;

                    }
                    result += Factors[i].ToString();
                }
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
                    if (Terms[i].Coefficient == 1)
                        result += Terms[i].Variable.Identifier + (Terms[i].Exponent == 1 ? "" : "^" + Terms[i].Exponent);
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

        public string ToStringExpression() => ToStringExpression(false, true);
        public override string ToString() => ToStringExpression(false, true);

        public string Designation;
        public string[] DependentVariables;
        public PolynomialTerm[] Terms;
        public double Constant = 0;
        public int Degree { get
            {
                if (Terms == null) return 0;
                return Terms.Length;
            } }

        Expressions.EnclosedExpression[] Factors;   // This could be changed to an multiplicationExpression
        
        public void Factorize(bool preserveShape = true)
        {
            if (Degree > 2)
            {
                Factors = new Expressions.EnclosedExpression[Terms.Length];
                PolynomialBase residualPolynomial = this;

                Variables.Variable qualifiedGuessVar;
                double qualifiedGuess = 0;
                for (int i = 0; i < Terms.Length; i++)
                {
                    qualifiedGuessVar = new Variables.Variable(Terms[i].Variable.Identifier, qualifiedGuess);
                    double root = NewtonRaphsonMethod(residualPolynomial, qualifiedGuessVar);  // Can be optimized, make degree check here and pick out all roots at the same time 
                    IExpression factorExpression;
                    if (root > 0)
                        factorExpression = new Expressions.SubtractionExpression(
                        Terms[i].Variable,
                        new Expressions.ConstantExpression(root)
                        );
                    else if (root < 0)
                        factorExpression = new Expressions.AdditionExpression(
                            Terms[i].Variable,
                            new Expressions.ConstantExpression(root * -1)
                            );
                    else
                        factorExpression = Terms[i].Variable;
                    Factors[i] = new Expressions.EnclosedExpression(factorExpression);


                    // Divide polynomial with root
                    Classes.Polynomial rootPolynomial = new Classes.Polynomial(new PolynomialTerm[] { new PolynomialTerm(1, Terms[i].Variable, 1) }, root); // Can be optimized
                    residualPolynomial = residualPolynomial / rootPolynomial;
                }
            }
            else if (Degree == 2)
            {
                double[] roots = QuadraticFormula(this);
                Factors = new Expressions.EnclosedExpression[roots.Length + (preserveShape ? 1 : 0)];

                if (preserveShape)
                    Factors[0] = new Expressions.EnclosedExpression(new Expressions.ConstantExpression(Terms[0].Coefficient)); // Check again

                for (int i = 0; i < roots.Length; i++)
                {
                    double root = roots[i];
                    IExpression factorExpression;
                    if (root > 0)
                        factorExpression = new Expressions.SubtractionExpression(
                        Terms[i].Variable,
                        new Expressions.ConstantExpression(root)
                        );
                    else if (root < 0)
                        factorExpression = new Expressions.AdditionExpression(
                            Terms[i].Variable,
                            new Expressions.ConstantExpression(root * -1)
                            );
                    else
                        factorExpression = Terms[i].Variable;
                    Factors[i + (preserveShape ? 1 : 0)] = new Expressions.EnclosedExpression(factorExpression);
                }
            }
            else // Degree < 2
            {
                throw new NotImplementedException();
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


        public static double[] QuadraticFormula(PolynomialBase polynomial)
        {
            // Use the Quadratic Formula
            double a = 0;
            double b = 0;
            double c = polynomial.Constant;

            if (polynomial.Terms.Length == 2)
            {
                a = polynomial.Terms[0].Coefficient;
                b = polynomial.Terms[1].Coefficient;
            }
            else if (polynomial.Terms.Length == 1)
                b = polynomial.Terms[0].Coefficient;

            return new double[] {
                (-b + System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a),
                (-b - System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a)
            };
            
        }
        // Newton's method
        public static double NewtonRaphsonMethod(PolynomialBase polynomial, Variables.Variable initailValue, double threshold = 0.00001d)
        {
            if (!initailValue.Value.HasValue) throw new NotFiniteNumberException("The initial value is not defined!");
            double xn_pre = double.NaN;
            double error = double.NaN;

            PolynomialBase derivative = polynomial.Derivative();
            if (polynomial.Degree < 1) throw new NotFiniteNumberException("The polynomial " + polynomial + " has no (real) roots.");

            while(xn_pre.Equals(double.NaN) || error > threshold)
            {
                IExpression funcVal = polynomial.Evaluate(initailValue);
                IExpression deriVal = derivative.Evaluate(initailValue);

                if (!(funcVal is Expressions.ConstantExpression && deriVal is Expressions.ConstantExpression)) throw new ArgumentNullException("Too few initial values");

                xn_pre = initailValue.Value.Value;
                initailValue.Value = xn_pre - ((Expressions.ConstantExpression)funcVal).Value / ((Expressions.ConstantExpression)deriVal).Value;

                error = System.Math.Abs(xn_pre - initailValue.Value.Value);
            }
                
            return initailValue.Value.Value;
        }

        public IExpression Evaluate(Variables.Variable variable) => Evaluate(new Variables.VariableCollection(variable));
        public IExpression Evaluate(params Variables.Variable[] variables) => Evaluate((Variables.VariableCollection)variables);
        public IExpression Evaluate(Variables.VariableCollection variables)
        {
            if (Degree > 0)
            {
                bool constantTerms = true;
                IExpression result;
                result = new Expressions.AdditionExpression(null, Constant);

                Expressions.AdditionExpression node = result as Expressions.AdditionExpression;
                for (int i = 0; i < Terms.Length; i++)
                {
                    if (i == Terms.Length - 1)
                        node.Term1 = Terms[i].Evaluate(variables);
                    else
                    {
                        IExpression termExpr = Terms[i].Evaluate(variables);
                        if (!(termExpr is Expressions.ConstantExpression)) constantTerms = false;
                        node.Term1 = new Expressions.AdditionExpression(null, termExpr);
                        node = node.Term1 as Expressions.AdditionExpression;
                    }
                }

                if (constantTerms) result = result.Evaluate(variables); // Evaluate constant addition again

                return result;
            }
            else
                return new Expressions.ConstantExpression(Constant);
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
