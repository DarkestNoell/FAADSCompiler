namespace Compiler
{
    public class MathNode : TreeNode
    {
        public enum MathSymbol
        {
            Plus, Minus, Divide, Multiply
        }

        public MathSymbol GetMathFromString(string math)
        {
            MathSymbol mathSymbol = MathSymbol.Plus;
            switch (math)
            {
                case "+":
                    mathSymbol = MathSymbol.Plus;
                    break;
                case "-":
                    mathSymbol = MathSymbol.Minus;
                    break;
                case "/":
                    mathSymbol = MathSymbol.Divide;
                    break;
                case "*":
                    mathSymbol = MathSymbol.Multiply;
                    break;
            }
            return mathSymbol;
        }

        private MathSymbol symbol;

        public MathNode(string symbol)
        {
            this.symbol = GetMathFromString(symbol);
        }

        public MathSymbol GetMathSymbol()
        {
            return symbol;
        }
    }
}