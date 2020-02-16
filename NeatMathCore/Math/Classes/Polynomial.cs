using System.Collections.Generic;

namespace NeatMathCore.Math.Classes
{
    class Polynomial : Models.PolynomialBase
    {
        public Polynomial(Models.PolynomialTerm[] terms, double constant = 0, string variable = "x") : this(null, terms, constant, variable) { }
        public Polynomial(string designation, Models.PolynomialTerm[] terms, double constant = 0, string variable = "x")
        {
            Designation = designation;
            List<Models.PolynomialTerm> termsList = new List<Models.PolynomialTerm>();
            for (int i = 0; i < terms.Length; i++)
            {
                if (terms[i].Coefficient != 0) termsList.Add(terms[i]);
            }
            Terms = termsList.ToArray();
            Constant = constant;
        }

        public Polynomial(double[] coefficients, double constant = 0, string variable = "x") : this(null, coefficients, constant, variable) { }
        public Polynomial(string designation, double[] coefficients, double constant = 0, string variable = "x")
        {
            Designation = designation;
            List<Models.PolynomialTerm> termsList = new List<Models.PolynomialTerm>();
            for (int i = 0; i < coefficients.Length; i++)
            {
                // Bug when coefficient is 0
                if (coefficients[i] != 0) termsList.Add(
                    new Models.PolynomialTerm(coefficients[i], new Models.Expressions.VariableExpression(variable), coefficients.Length - i)
                );
            }
            Terms = termsList.ToArray();
            Constant = constant;
        }

        public Polynomial(int degree) : this(null, degree) { }
        public Polynomial(string designation, int degree)
        {
            Designation = designation;
            Terms = new Models.PolynomialTerm[degree];
        }
    }
}
