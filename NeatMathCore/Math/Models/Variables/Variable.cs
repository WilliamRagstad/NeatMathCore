using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Variables
{
    class Variable
    {
        public Variable(string identifier, double? value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; }
        public double? Value;

        public override string ToString() => Identifier + "=" + Value;
    }
}
