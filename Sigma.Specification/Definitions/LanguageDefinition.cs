using System;
using System.Collections.Generic;

namespace Sigma.Specification
{
    /// <summary>
    /// Defines Language
    /// </summary>
    public class LanguageDefinition
    {
        /// <summary>
        /// Definition of language metadata
        /// </summary>
        public LanguageMetadataDefinition Metadata { get; set; }

        /// <summary>
        /// Logical operator definitions identified by opcode
        /// </summary>
        private Dictionary<string, LogicalOperatorDefinition> LogicalOperators { get; }

        /// <summary>
        /// Predicate operator definitions identified by opcode
        /// </summary>
        private Dictionary<string, PredicateOperatorDefinition> PredicateOperators { get; }

        /// <summary>
        /// Name of language definition
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LanguageDefinition()
        {
            Metadata = new LanguageMetadataDefinition();
            LogicalOperators = new Dictionary<string, LogicalOperatorDefinition>();
            PredicateOperators = new Dictionary<string, PredicateOperatorDefinition>();
            Name = string.Empty;
        }

        /// <summary>
        /// Get logical operator from definition
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public LogicalOperatorDefinition GetLogicalOperator(string opcode)
        {
            if (LogicalOperators.ContainsKey(opcode))
            {
                return LogicalOperators[opcode];
            }

            // throw on problem
            throw new Exception($"Logical Operator with code ({opcode}) is not defined");
        }

        /// <summary>
        /// Get predicate operator from definition
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public PredicateOperatorDefinition GetPredicateOperator(string opcode)
        {
            if (PredicateOperators.ContainsKey(opcode))
            {
                return PredicateOperators[opcode];
            }

            // throw on problem
            throw new Exception($"Predicate Operator with code ({opcode}) is not defined");
        }

        /// <summary>
        /// Add logical operator
        /// </summary>
        /// <param name="operator"></param>
        public void AddLogicalOperator(LogicalOperatorDefinition @operator)
        {
            // key of operator
            var key = @operator.Operator;

            // check existance
            if (LogicalOperators.ContainsKey(key))
            {
                throw new Exception($"Operator with code {key} is already defined");
            }

            // register operator
            LogicalOperators.Add(key, @operator);
        }

        /// <summary>
        /// Add predicate operator
        /// </summary>
        /// <param name="operator"></param>
        public void AddPredicateOperator(PredicateOperatorDefinition @operator)
        {
            // key of operator
            var key = @operator.Operator;

            // check existance
            if (PredicateOperators.ContainsKey(key))
            {
                throw new Exception($"Operator with code {key} is already defined");
            }

            // register operator
            PredicateOperators.Add(key, @operator);
        }
    }
}