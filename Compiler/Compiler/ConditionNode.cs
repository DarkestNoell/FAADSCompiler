namespace Compiler
{
    public class ConditionNode : TreeNode
    {
        public enum ConditionSymbol
        {
            Equals, NotEquals, GreaterThan, LessThan, GreaterThanOrEqual, LessThanOrEqual
        }

        public ConditionSymbol GetConditionFromString(string condition)
        {
            ConditionSymbol mathSymbol = ConditionSymbol.Equals;
            switch (condition)
            {
                case "==":
                    mathSymbol = ConditionSymbol.Equals;
                    break;
                case "<=":
                    mathSymbol = ConditionSymbol.LessThanOrEqual;
                    break;
                case ">=":
                    mathSymbol = ConditionSymbol.GreaterThanOrEqual;
                    break;
                case ">":
                    mathSymbol = ConditionSymbol.GreaterThan;
                    break;
                case "<":
                    mathSymbol = ConditionSymbol.LessThan;
                    break;
            }
            return mathSymbol;
        }

        private ConditionSymbol symbol;

        public ConditionNode(string symbol)
        {
            this.symbol = GetConditionFromString(symbol);
        }

        public ConditionSymbol GetConditionSymbol()
        {
            return symbol;
        }
    }
}