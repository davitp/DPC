using System.Collections.Generic;

namespace DPC.Model
{
    /// <summary>
    /// Predicate tree node interface
    /// </summary>
    public interface IFormulaNode
    {
        /// <summary>
        /// Parent Node
        /// </summary>
        IFormulaNode Parent { get; set; }

        /// <summary>
        /// Child nodes 
        /// </summary>
        IEnumerable<IFormulaNode> Children { get; set; } 
    }
}
