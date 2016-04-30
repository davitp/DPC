using System;
using System.Collections.Generic;

namespace Sigma.Specification
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
        private static LanguageDefinitionRepository instance;

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
            throw new Exception($"Language Definition with name ({language}) is not defined");
        }

        /// <summary>
        /// Register language definition
        /// </summary>
        /// <param name="definition"></param>
        public void RegisterLanguageDefinition(LanguageDefinition definition)
        {
            // check for existance
            if (LanguageDefinitions.ContainsKey(definition.Name))
            {
                throw  new Exception($"Language with name {definition.Name} is already registered");
            }

            // add 
            LanguageDefinitions.Add(definition.Name, definition);
        }
    }
}