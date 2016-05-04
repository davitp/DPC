using System.Collections.Generic;

namespace Sigma.Model
{
    /// <summary>
    ///     Model of operand that defines variable
    /// </summary>
    public class Variable : Operand
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Variable(Predicate predicate) : this()
        {
            Parent = predicate;
        }

        /// <summary>
        ///     Parameterless constructor
        /// </summary>
        public Variable()
        {
            Children = new List<IFormulaNode>();
        }
    }
}