using System;
using System.Collections.Generic;
using System.Text;

namespace NeatMathCore.Math.Models.Variables
{
    class VariableCollection
    {
        public VariableCollection(params Variable[] variables)
        {
            Variables = variables;
        }
        public VariableCollection(int capacity) {
            Variables = new Variable[capacity];
        }

        public Variable[] Variables { get; }

        public bool Contains(Variable variable)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i].Equals(variable)) return true;
            }
            return false;
        }

        public bool Contains(string variableIdentifier)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i].Identifier == variableIdentifier) return true;
            }
            return false;
        }

        public Variable Get(string variableIdentifier)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i].Identifier == variableIdentifier) return Variables[i];
            }
            return null;
        }
    }
}
