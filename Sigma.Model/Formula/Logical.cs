using System.Collections.Generic;

namespace Sigma.Model
{
    /// <summary>
    ///     Logical operation definition
    ///     Defines connectivity between Predicates and other logical operatoins
    ///     Example: AND, OR, NOT
    /// </summary>
    public class Logical : IFormulaNode, IFormulaOperator
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="parent"></param>
        public Logical(Logical parent) : this()
        {
            Parent = parent;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public Logical()
        {
            Children = new List<IFormulaNode>();
        }

        /// <summary>
        ///     Child nodes of logical tree
        /// </summary>
        public IList<IFormulaNode> Children { get; set; }

        /// <summary>
        ///     Parent node of logical subtree
        /// </summary>
        public IFormulaNode Parent { get; set; }

        /// <summary>
        ///     OpCode of Logical operator
        /// </summary>
        public string OpCode { get; set; }
    }
}