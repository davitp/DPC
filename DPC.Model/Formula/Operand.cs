using System.Collections.Generic;
using System.Linq;


namespace DPC.Model
{
    /// <summary>
    /// Class defines operand of predicate
    /// </summary>
    public class Operand : IFormulaNode
    {
        /// <summary>
        /// Children operand (should be empty)
        /// </summary>
        public IEnumerable<IFormulaNode> Children { get; set; }

        /// <summary>
        /// Parent of operand (should be predicate)
        /// </summary>
        public IFormulaNode Parent { get; set; }

        /// <summary>
        /// Operand value definition
        /// </summary>
        public object Value { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Operand(Predicate predicate) : this()
        {
            Parent = predicate;
        }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Operand()
        {
            Children = Enumerable.Empty<IFormulaNode>();
        }
    }
}
