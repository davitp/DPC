using Sigma.Model;

namespace Sigma.Processor
{
    /// <summary>
    /// API that provides formula processing functionality
    /// </summary>
    public interface IFormulaProcessor
    {
        /// <summary>
        /// Process formula
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        string Process(Formula formula);

        /// <summary>
        /// Language
        /// </summary>
        string Language { get; }
    }
}
