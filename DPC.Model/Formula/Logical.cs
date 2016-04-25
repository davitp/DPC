﻿using System.Collections.Generic;

namespace DPC.Model
{
    /// <summary>
    /// Logical operation definition
    /// Defines connectivity between Predicates and other logical operatoins
    /// Example: AND, OR, NOT
    /// </summary>
    public class Logical : IFormulaNode
    {
        /// <summary>
        /// Child nodes of logical tree
        /// </summary>
        public IEnumerable<IFormulaNode> Children { get; set; }

        /// <summary>
        /// Parent node of logical subtree
        /// </summary>
        public IFormulaNode Parent { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public Logical(Logical parent) : this()
        {
            Parent = parent;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Logical()
        {
            Children = new List<IFormulaNode>();
        }
    }
}