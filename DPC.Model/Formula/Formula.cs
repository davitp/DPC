using System;

namespace DPC.Model
{
    /// <summary>
    /// Definition of formula
    /// 1. Predicate with operands is formula
    /// 2. Logical operators with Predicate operands 
    /// </summary>
    public class Formula
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Formula()
        {
            Tree = null;
        }

        /// <summary>
        /// Expression tree node
        /// </summary>
        public IFormulaNode Tree { get; }

        /// <summary>
        /// Traverse on tree and apply action on each node
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<IFormulaNode> action)
        {
            // traverse on tree if root is not null
            if (Tree != null)
            {
                TraverseImplementation(action, Tree);
            }
        }

        /// <summary>
        /// Recursive implementation of traverse operation
        /// </summary>
        /// <param name="action"></param>
        /// <param name="node"></param>
        private void TraverseImplementation(Action<IFormulaNode> action, IFormulaNode node)
        {
            // touch root
            action(node);

            // iterate on children
            foreach (var child in node.Children)
            {
                // traverse on each child
                TraverseImplementation(action, child);
            }
        }
    }
}
