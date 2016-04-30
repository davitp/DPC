namespace Sigma.Expressions
{
    /// <summary>
    /// Encapsulates OpCode constants
    /// </summary>
    public class OpCodes
    {
        /// <summary>
        /// Logical operation AND
        /// </summary>
        public static string And = "__And";

        /// <summary>
        /// Logical Operator OR
        /// </summary>
        public static string Or = "__Or";

        /// <summary>
        /// Logical Operator NOT
        /// </summary>
        public static string Not = "__Not";

        /// <summary>
        /// Predicate Equal
        /// </summary>
        public static string Equal = "__Equal";

        /// <summary>
        /// Predicate Not Equal
        /// </summary>
        public static string NotEqual = "__NotEqual";

        /// <summary>
        /// Predicate "Is Less than"
        /// </summary>
        public static string Less = "__Less";

        /// <summary>
        /// Predicate "Is greater than"
        /// </summary>
        public static string Greater = "__Greater";

        /// <summary>
        /// Predicate "Is less or equal"
        /// </summary>
        public static string LessOrEqual = "__LessOrEqual";

        /// <summary>
        /// Predicate "Is greater or equal"
        /// </summary>
        public static string GreaterOrEqual = "__GreaterOrEqual";

        /// <summary>
        /// Predicate "Is between"
        /// </summary>
        public static string Between = "__Between";

        /// <summary>
        /// Predicate "Has Substring"
        /// </summary>
        public static string HasSubstring = "__HasSubstring";

        /// <summary>
        /// Predicate "Is substring of"
        /// </summary>
        public static string IsSubstrings = "__IsSubstring";

        /// <summary>
        /// Predicate "Is Zero"
        /// </summary>
        public static string IsZero = "__IsZero";

        /// <summary>
        /// Predicate "Is In"
        /// </summary>
        public static string In = "__In";

        /// <summary>
        /// Predicate "Absolute truth"
        /// </summary>
        public static string Truth = "__Truth";

        /// <summary>
        /// Predicate "Absolute Falsehood"
        /// </summary>
        public static string Falsehood = "__Falsehood";
    }
}