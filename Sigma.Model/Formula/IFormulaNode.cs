using System.Collections.Generic;

namespace Sigma.Model
{
    /// <summary>
    ///     Predicate tree node interface
    /// </summary>
    public interface IFormulaNode
    {
        /// <summary>
        ///     Parent Node
        /// </summary>
        IFormulaNode Parent { get; set; }

        /// <summary>
        ///     Child nodes
        /// </summary>
        IList<IFormulaNode> Children { get; set; }
    }
}