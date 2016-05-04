using System;

namespace Sigma.Model
{
    /// <summary>
    ///     Definition of formula
    ///     1. Predicate with operands is formula
    ///     2. Logical operators with Predicate operands
    /// </summary>
    public class Formula
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Formula(IFormulaNode tree)
        {
            // assign tree
            Tree = tree;

            // set parent child relationship
            // Note, that relationship is only valid in formula context
            SetRelationship(Tree);
        }

        /// <summary>
        /// Set parent-child relation ship between root and children
        /// </summary>
        /// <param name="root"></param>
        private void SetRelationship(IFormulaNode root)
        {
            // check for root correctness
            if (root == null)
            {
                return;
            }

            // iterate over children
            foreach (var child in root.Children)
            {
                // set relationship of child
                SetRelationship(child);
            }
        }

        /// <summary>
        ///     Expression tree node
        /// </summary>
        public IFormulaNode Tree { get; }

        /// <summary>
        ///     Traverse on tree and apply action on each node
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
        ///     Recursive implementation of traverse operation
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