using Sigma.Model;
using System.Collections.Generic;

namespace Sigma.Expressions
{
    /// <summary>
    ///     Helper class to build children array from given arguments
    /// </summary>
    internal class ChildrenBuilder
    {
        /// <summary>
        /// Transform given parameters into children list 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static IList<IFormulaNode> Transform(params object[] parameters)
        {
            // prepare collection for children
            var children = new List<IFormulaNode>(parameters.Length);

            // iterate over parameters
            foreach (var param in parameters)
            {
                if (param is IFormulaNode)
                {
                    children.Add(param as IFormulaNode);
                }
                else
                {
                    children.Add(new Operand
                    {
                        Value = param
                    });
                }
            }

            // return result
            return children;
        }
    }
}