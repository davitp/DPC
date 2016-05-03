using System;
using System.Collections.Generic;
using Sigma.Model;

namespace Sigma.Expressions
{
    /// <summary>
    ///     Defines Expression class interface
    /// </summary>
    public class SigExp
    {
        /// <summary>
        /// Shortcut to build AND tree
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IFormulaNode And(IFormulaNode left, IFormulaNode right)
        {
            // create new logical node and return
            return new Logical()
            {
                OpCode = OpCodes.And,
                Children = new List<IFormulaNode>() { left, right}
            };
        }

        /// <summary>
        /// Shortcut to build OR tree
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IFormulaNode Or(IFormulaNode left, IFormulaNode right)
        {
            // create new logical node and return
            return new Logical()
            {
                OpCode = OpCodes.Or,
                Children = new List<IFormulaNode>() { left, right }
            };
        }

        /// <summary>
        /// Shortcut to build Equal predicate
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IFormulaNode Equal(object left, object right)
        {
            return new Predicate()
            {
                OpCode = OpCodes.Equal,
                Children = new List<IFormulaNode>()
                {
                    new Operand()
                    {
                        Value = left
                    },

                    new Operand()
                    {
                        Value = right
                    }
                }
            };
        }
    }
}