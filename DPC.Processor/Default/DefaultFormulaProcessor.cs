using System;
using DPC.Model;
using DPC.Processor.API;

namespace DPC.Processor.Default
{
    /// <summary>
    /// Default Formula Processor
    /// Can be extended to meet custom requirements
    /// </summary>
    public class DefaultFormulaProcessor : IFormulaProcessor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="language"></param>
        public DefaultFormulaProcessor(string language)
        {
            Language = language;
        }

        /// <summary>
        /// Language of processor
        /// </summary>
        public string Language { get; }
        
        /// <summary>
        /// Process formula to generated code
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public string Process(Formula formula)
        {
            throw new NotImplementedException();
        }
    }
}