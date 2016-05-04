using System.Collections.Generic;

namespace Sigma.Model
{
    /// <summary>
    ///     Model of operand that defines constant
    /// </summary>
    public class Constant : Operand
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Constant(Predicate predicate) : this()
        {
            Parent = predicate;
        }

        /// <summary>
        ///     Parameterless constructor
        /// </summary>
        public Constant()
        {
            Children = new List<IFormulaNode>();
        }
    }
}