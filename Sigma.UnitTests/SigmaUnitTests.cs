using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sigma.Model;
using Sigma.Processor;
using Sigma.Expressions;
using System;

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

            // Register Arithmetic Langauge
            FormulaProcessorRepository.Instance.RegisterFormulaProcessor(new DefaultFormulaProcessor("Arithmetic"));
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
            (
                new Logical
                {
                    OpCode = "__Or",
                    Children = new List<IFormulaNode>
                    {
                        subtree1,
                        subtree3
                    }
                }
            );

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


        /// <summary>
        /// Basic test for expression
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ExpressionBasicTest()
        {
            var a = 4;
            var b = 5;
            var c = 6;
            var d = 4;

            // corresponding C# code
            // a == b && (b == c || a == d)
            var formula = new Formula
            (
                SigExp.And
                (
                    SigExp.Equal(a, b),
                    SigExp.Or
                    (
                        SigExp.Not
                        (
                            SigExp.Equal(b, c)
                        ),
                        SigExp.Equal(a, d)
                    )
                )
            );
            

            var sql = FormulaProcessorRepository
                .Instance
                .GetFormulaProcessor("SQL");

            var armenian = FormulaProcessorRepository
                .Instance
                .GetFormulaProcessor("Armenian");

            var resultAM = armenian.Process(formula);

            var resultSQL = sql.Process(formula);

            Assert.IsNotNull(resultAM);
            Assert.IsNotNull(resultSQL);
        }

        /// <summary>
        /// Basic test for arithmetic language
        /// </summary>
        [TestMethod]
        public void ArithmeticBasicTest()
        {
            var a = 4;
            var b = 5;
            var c = 6;
            var d = 4;

            var arithmetic = FormulaProcessorRepository
                .Instance
                .GetFormulaProcessor("Arithmetic");

            // b = c
            var formula = new Formula
            (
                SigExp.Equal(b, c)
            );

            var resultArithmetic = arithmetic.Process(formula);

            Assert.IsNotNull(resultArithmetic);


            formula = new Formula
            (
                SigExp.Less(b, d)
            );

            resultArithmetic = arithmetic.Process(formula);

            Assert.IsNotNull(resultArithmetic);

            formula = new Formula
            (
                SigExp.IsZero(a)
            );

            resultArithmetic = arithmetic.Process(formula);

            Assert.IsNotNull(resultArithmetic);
        }
    }
}