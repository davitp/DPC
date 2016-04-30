using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sigma.Model;
using Sigma.Processor;

namespace Sigma.UnitTests
{
    /// <summary>
    ///     Main unit test class
    /// </summary>
    [DeploymentItem(PathPrefix + @"\" + LanguageDefinitionsPath)]
    [TestClass]
    public class SigmaUnitTests
    {
        /// <summary>
        ///     Prefix of path containing the XSD and XML definitions for testing
        /// </summary>
        public const string PathPrefix = "Definitions";

        /// <summary>
        ///     Language definitions file path
        /// </summary>
        public const string LanguageDefinitionsPath = "LanguageDefinitions.xml";

        /// <summary>
        ///     Unitialize Unit Test
        /// </summary>
        public SigmaUnitTests()
        {
            // Initialize specification by XML path
            Specification.Specification.Initialize(LanguageDefinitionsPath);

            // Register SQL Language
            FormulaProcessorRepository.Instance.RegisterFormulaProcessor(new DefaultFormulaProcessor("SQL"));

            // Register Armenian Language
            FormulaProcessorRepository.Instance.RegisterFormulaProcessor(new DefaultFormulaProcessor("Armenian"));
        }

        /// <summary>
        ///     Combined unit test method
        ///     Languages should be processed in basic level
        /// </summary>
        [TestMethod]
        public void CombinedTest()
        {
            var subtree1 = new Predicate
            {
                OpCode = "__Equal",
                Parent = null,
                Children = new List<IFormulaNode>
                {
                    new Operand
                    {
                        Value = 1
                    },
                    new Operand
                    {
                        Value = 1
                    }
                }
            };

            var subtree2 = new Predicate
            {
                OpCode = "__Less",
                Parent = null,
                Children = new List<IFormulaNode>
                {
                    new Operand
                    {
                        Value = 1
                    },
                    new Operand
                    {
                        Value = 2
                    }
                }
            };

            var subtree3 = new Logical
            {
                OpCode = "__And",
                Children = new List<IFormulaNode>
                {
                    subtree1,
                    subtree2
                }
            };


            var formula = new Formula
            {
                Tree = new Logical
                {
                    OpCode = "__Or",
                    Children = new List<IFormulaNode>
                    {
                        subtree1,
                        subtree3
                    }
                }
            };

            var armenian = FormulaProcessorRepository
                .Instance
                .GetFormulaProcessor("Armenian");

            var sql = FormulaProcessorRepository
                .Instance
                .GetFormulaProcessor("SQL");

            var resultAM = armenian.Process(formula);

            var resultSQL = sql.Process(formula);

            Assert.IsNotNull(resultAM);
            Assert.IsNotNull(resultSQL);
        }
    }
}