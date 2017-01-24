using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class SymbolTableNode
    {
        private SymbolTableNode Parent;
        private SymbolTableNode RightChild;
        private SymbolTableNode LeftChild;
        private Dictionary<int, Symbol> SymbolTableData = new Dictionary<int, Symbol>();
        public SymbolTableNode(SymbolTableNode parent, SymbolTableNode rightChild, SymbolTableNode leftChild)
        {
            this.Parent = parent;
            this.RightChild = rightChild;
            this.LeftChild = leftChild;
        }
    }
}
