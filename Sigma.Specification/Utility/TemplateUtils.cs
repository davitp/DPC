using System.Linq;
using System.Text.RegularExpressions;

namespace Sigma.Specification
{
    /// <summary>
    /// Utitlities for templated strings
    /// </summary>
    public static class TemplateUtils
    {
        /// <summary>
        /// Count arguments in templated string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int CountTempaltedArgs(this string input)
        {
            return Regex.Matches(input, @"{(.*?)}").OfType<Match>().Select(m => m.Value).Distinct().Count();
        }
    }
}