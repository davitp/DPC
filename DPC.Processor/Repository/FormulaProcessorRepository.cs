using System;
using System.Collections.Generic;
using DPC.Processor.API;

namespace DPC.Processor.Repository
{
    /// <summary>
    /// Formula processor repository
    /// </summary>
    public class FormulaProcessorRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FormulaProcessorRepository()
        {
            FormulaProcessors = new Dictionary<string, IFormulaProcessor>();
        }

        /// <summary>
        /// Internal single instance
        /// </summary>
        private static FormulaProcessorRepository instance;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static FormulaProcessorRepository Instance => instance ?? (instance = new FormulaProcessorRepository());

        /// <summary>
        /// Formula processors mapping by language
        /// </summary>
        private Dictionary<string, IFormulaProcessor> FormulaProcessors { get; }

        /// <summary>
        /// Register formula processor
        /// </summary>
        /// <param name="processor"></param>
        public void RegisterFormulaProcessor(IFormulaProcessor processor)
        {
            // get language
            var language = processor.Language;
            
            if (FormulaProcessors.ContainsKey(language))
            {
                throw new Exception($"Processor for language {language} is already registered");
            }

            FormulaProcessors.Add(language, processor);
        }

        /// <summary>
        /// Get Formula processor for given language
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="language"></param>
        /// <returns></returns>
        public T GetFormulaProcessor<T>(string language)  where T : IFormulaProcessor
        {
            if (!FormulaProcessors.ContainsKey(language))
            {
                throw new Exception($"Language processor {language} is not registered");
            }

            return (T)FormulaProcessors[language];
        }

        /// <summary>
        /// Get formula processor for given language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public IFormulaProcessor GetFormulaProcessor(string language)
        {
            return GetFormulaProcessor<IFormulaProcessor>(language);
        }
    }
}