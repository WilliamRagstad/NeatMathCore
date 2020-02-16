using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models
{
    abstract class Expression
    {
        public Expression() { }

        public double Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
