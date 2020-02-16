using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Util.Structure
{
    class PolynomialFactor
    {
        public PolynomialFactor(char variable, double constant)
        {
            Variable = variable;
            Constant = constant;
        }

        public char Variable { get; }
        public double Constant { get; }

        public override string ToString()
        {
            if (Constant < 0)
                return $"({Variable} - {-Constant})";
            else if (Constant == 0) return Variable.ToString();
            else return $"({Variable} + {Constant})";
        }
    }
}
