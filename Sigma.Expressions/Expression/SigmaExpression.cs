using Sigma.Model;

namespace Sigma.Expressions
{
    /// <summary>
    ///     Defines Expression class interface
    /// </summary>
    public class SigmaExpression
    {
        /// <summary>
        ///     Parameterless constructor
        /// </summary>
        public SigmaExpression()
        {
            InnerFormula = new Formula();
        }

        /// <summary>
        ///     Internal Formula object corresponding to SigmaExpression
        /// </summary>
        protected Formula InnerFormula { get; set; }
    }
}