namespace Compiler
{
    public class MathNode : TreeNode
    {
        public enum MathSymbol
        {
            Plus, Minus, Divide, Multiply
        }

        private MathSymbol symbol;

        public MathNode(MathSymbol symbol)
        {
            this.symbol = symbol;
        }

        public MathSymbol GetMathSymbol()
        {
            return symbol;
        }
    }
}