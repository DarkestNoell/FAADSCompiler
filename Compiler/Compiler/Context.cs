﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Context
    {

        private string name;
        
        private VariableType.Type variableType;

        private bool function;

        //Context defaults as a variable. Add true at the end of constructor to set function
        public Context(string name, VariableType.Type variableType, bool function = false)
        {
            this.name = name;
            this.variableType = variableType;
            this.function = function;
        }

        public string GetName()
        {
            return name;
        }

        public VariableType.Type GetVariableType()
        {
            return variableType;
        }

        public bool IsFunction()
        {
            return function;
        }

    }
}
