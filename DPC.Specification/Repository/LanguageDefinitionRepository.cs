using System;
using System.Collections.Generic;
using DPC.Specification.Definitions;

namespace DPC.Specification.Repository
{
    /// <summary>
    /// Language Definition Repository
    /// </summary>
    public class LanguageDefinitionRepository
    {
        /// <summary>
        /// Default Constructor 
        /// </summary>
        public LanguageDefinitionRepository()
        {
            LanguageDefinitions = new Dictionary<string, LanguageDefinition>();
        }

        /// <summary>
        /// Single instance internals
        /// </summary>
        private static LanguageDefinitionRepository instance = null;

        /// <summary>
        /// Get single instance of repository
        /// </summary>
        public static LanguageDefinitionRepository Instance => instance ?? (instance = new LanguageDefinitionRepository());

        /// <summary>
        /// Logical operator mappings by language
        /// </summary>
        private Dictionary<string, LanguageDefinition> LanguageDefinitions { get; }

        /// <summary>
        /// Ask Repository for language definition
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public LanguageDefinition GetLanguageDefinition(string language)
        {
            if (LanguageDefinitions.ContainsKey(language))
            {
                return LanguageDefinitions[language];
            }

            // throw on problem
            throw new Exception($"Logical Operator with code ({language}) is not defined");
        }
    }
}