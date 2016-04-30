namespace Sigma.Specification
{
    /// <summary>
    /// Base definition structure. 
    /// Common part for both logical and predicate operator definitions
    /// </summary>
    public class BaseOperatorDefinition
    {
        /// <summary>
        /// Definition of operator
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Templated Implementation for operator
        /// </summary>
        public string Implementation { get; set; }

        /// <summary>
        /// Dimention of operator
        /// </summary>
        public int Dimention { get; set; }

        /// <summary>
        /// Base constructor
        /// </summary>
        public BaseOperatorDefinition()
        {
            Operator = string.Empty;
            Implementation = string.Empty;
            Dimention = 0;
        }

    }
}
