namespace Sigma.Processor
{
    /// <summary>
    /// Processor layer representative
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Initialize processor layer
        /// </summary>
        /// <param name="path"></param>
        public static void Initialize(string path)
        {
            Specification.Specification.Initialize(path);
        }
    }
}