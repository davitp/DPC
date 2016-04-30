using System;
using System.Linq;
using DPC.Model;
using DPC.Processor.API;
using DPC.Specification.Definitions;

// ReSharper disable TryCastAlwaysSucceeds

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
            LanguageDefinition = Specification
                .Repository
                .LanguageDefinitionRepository
                .Instance
                .GetLanguageDefinition(language);
        }

        /// <summary>
        /// Language of processor
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// Language definition cache 
        /// </summary>
        private LanguageDefinition LanguageDefinition { get; }

        /// <summary>
        /// Process formula to generated code
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public virtual string Process(Formula formula)
        {
            // get root node
            var root = formula.Tree;

            // Call implementation of process
            return ProcessImplementation(root);
        }

        /// <summary>
        /// Implementation of processing logic
        /// </summary>
        /// <param name="root"></param>
        private string ProcessImplementation(IFormulaNode root)
        {
            // handle case when node is predicate subtree
            if (root is Predicate)
            {
                return RepresentPredicate(root as Predicate);
            }

            // handle case when node is logical subtree
            if (root is Logical)
            {
                return RepresentLogical(root as Logical);
            }

            // handle case when node is operand
            if(root is Operand)
            {
                return RepresentOperand(root as Operand);
            }

            // redundant return
            return string.Empty;
        }

        /// <summary>
        /// Represent logical operator by language specific expression
        /// </summary>
        /// <param name="logical"></param>
        /// <returns></returns>
        private string RepresentLogical(Logical logical)
        {
            // get definition of logical
            var definition = LanguageDefinition
                .GetLogicalOperator(Specification.API.Specification.MapOpCode(logical.OpCode));

            // check correctness of children 
            if (definition.Dimention != logical.Children.Count)
            {
                throw new 
                    Exception($"Logical operator {logical.OpCode} requires {definition.Dimention} operands, but {logical.Children.Count} is passed");
            }

            // collection of predicates to be listed here
            var tokens = new object[definition.Dimention];

            // iterate over child nodes
            for (int i = 0; i < logical.Children.Count; i++)
            {
                var node = logical.Children[i];
                
                // handle logical subtree
                if (node is Logical)
                {
                    tokens[i] = RepresentLogical(node as Logical);
                }

                // handle predicate subtree
                if (node is Predicate)
                {
                    tokens[i] = RepresentPredicate(node as Predicate);
                }
            }

            // pure result of representation
            var representation = string.Format(definition.Implementation, tokens);

            // handle auto priority case
            if (LanguageDefinition.Metadata.Prioritizer == PrioritizerOption.AutoPrioritizer)
            {
                return representation;
            }

            if (LanguageDefinition.Metadata.Prioritizer == PrioritizerOption.BracketPrioritizer)
            {
                // put brackets inside
                return string.Concat(
                    LanguageDefinition.Metadata.OpenBracket,
                    representation, 
                    LanguageDefinition.Metadata.CloseBracket
                    );
            }

            return string.Empty;
        }

        /// <summary>
        /// Represent predicate by langauge specific expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private string RepresentPredicate(Predicate predicate)
        {
            // get arguments of predicate
            var args = predicate
                .Children
                .Select(operand => (object) RepresentOperand(operand as Operand))
                .ToArray();

            // get definition of predicate
            var definition = LanguageDefinition
                .GetPredicateOperator(Specification.API.Specification.MapOpCode(predicate.OpCode));

            // check for arguments correctness
            if (definition.Dimention != args.Length)
            {
                throw new Exception($"Predicate {predicate.OpCode} requires {definition.Dimention} arguments, but {args.Length} is passed");
            }

            return string.Format(definition.Implementation, args);
        }

        /// <summary>
        /// Represents operand using specification
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        private string RepresentOperand(Operand operand)
        {
            /* 
             * TODO: operand value should be also 
             * mapped from higher level to language specific representation 
             */
            return operand.Value.ToString();
        }
    }
}