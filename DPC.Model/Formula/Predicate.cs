using System;
using System.Collections.Generic;

namespace DPC.Model
{
    /// <summary>
    /// Class defines predicate
    /// </summary>
    public class Predicate : IFormulaNode, IFormulaOperator
    {
        /// <summary>
        /// Children of predicate
        /// </summary>
        public IList<IFormulaNode> Children { get; set; }

        /// <summary>
        /// Parent node in tree
        /// </summary>
        public IFormulaNode Parent { get; set; }

        /// <summary>
        /// OpCode of predicate
        /// </summary>
        public string OpCode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public Predicate(Logical parent)
            :this()
        {
            Parent = parent;
        }

        /// <summary>
        /// Construct predicate
        /// </summary>
        public Predicate()
        {
            Children = new List<IFormulaNode>();
        }

    }
}
