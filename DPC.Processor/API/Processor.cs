namespace DPC.Processor.API
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
            Specification.API.Specification.Initialize(path);
        }
    }
}