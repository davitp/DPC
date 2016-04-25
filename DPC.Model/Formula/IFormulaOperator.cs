namespace DPC.Model
{
    /// <summary>
    /// Formula Operator interface
    /// </summary>
    public interface IFormulaOperator
    {
        /// <summary>
        /// Operator should define an unique OpCode
        /// </summary>
        string OpCode { get; set; } 
    }
}