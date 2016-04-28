using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using DPC.Model;
using DPC.Processor.Default;
using DPC.Processor.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPC.UnitTests
{
    /// <summary>
    /// Main unit test class
    /// </summary>
    [DeploymentItem(PathPrefix + @"\" + LanguageDefinitionsPath)]
    [TestClass]
    public class DPCUnitTests
    {
        public DPCUnitTests()
        {
            OutputLocation = AppDomain.CurrentDomain.BaseDirectory;
            Specification.API.Specification.Initialize(LanguageDefinitionsPath);
        }

        /// <summary>
        /// Output location
        /// </summary>
        private string OutputLocation { get; set; }

        /// <summary>
        /// Prefix of path containing the XSD and XML definitions for testing
        /// </summary>
        public const string PathPrefix = "Definitions";

        /// <summary>
        /// Language definitions file path
        /// </summary>
        public const string LanguageDefinitionsPath = "LanguageDefinitions.xml";

        /// <summary>
        /// Dummy test method
        /// </summary>
        [TestMethod]
        public void DummyTest()
        {
            var subtree1 = new Predicate()
            {
                OpCode = "__Equal",
                Parent = null,
                Children = new List<IFormulaNode>()
                {
                    new Operand()
                    {
                        Value = 1
                    },
                    new Operand()
                    {
                        Value = 1
                    }
                }
            };

            var subtree2 = new Predicate()
            {
                OpCode = "__Less",
                Parent = null,
                Children = new List<IFormulaNode>()
                {
                    new Operand()
                    {
                        Value = 1
                    },
                    new Operand()
                    {
                        Value = 2
                    }
                }
            };

            var subtree3 = new Logical()
            {
                OpCode = "__And",
                Children = new List<IFormulaNode>()
                {
                    subtree1,
                    subtree2
                }
            };

            

            var formula = new Formula()
            {
                Tree = new Logical()
                {
                    OpCode = "__Or",
                    Children = new List<IFormulaNode>()
                    {
                        subtree1, subtree3
                    }
                }

        };

            FormulaProcessorRepository.Instance.RegisterFormulaProcessor(new DefaultFormulaProcessor("SQL"));
            var processor = FormulaProcessorRepository.Instance.GetFormulaProcessor("SQL");
            var result = processor.Process(formula);

        }
    }
}


