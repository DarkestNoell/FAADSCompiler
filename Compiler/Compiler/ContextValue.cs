using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ContextValue
    {
        private double value;
        private Context context;

        public ContextValue(Context context)
        {
            this.context = context;
            value = 0;
        }

        public void SetValue(double value)
        {
            this.value = value;
        }

        public double GetValue()
        {
            return value;
        }

        public Context GetContext()
        {
            return context;
        }
    }
}
