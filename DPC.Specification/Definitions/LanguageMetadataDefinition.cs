namespace DPC.Specification.Definitions
{
    /// <summary>
    /// Defines Language metadata
    /// </summary>
    public class LanguageMetadataDefinition
    {
        /// <summary>
        /// Name of language
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Open Bracket for language specific code generation
        /// </summary>
        public string OpenBracket { get; set; }

        /// <summary>
        /// Close Bracket for langauge specific code generation
        /// </summary>
        public string CloseBracket { get; set; }

        /// <summary>
        /// Prioritizing method. 
        /// For example: Using brackets for defining priority or using automatic order
        /// </summary>
        public PrioritizerOption Prioritizer { get; set; }
    }
}