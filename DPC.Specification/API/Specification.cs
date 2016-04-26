using System;
using System.IO;

namespace DPC.Specification.API
{
    /// <summary>
    /// Specification layer representative
    /// </summary>
    public class Specification
    {
        /// <summary>
        /// Initialize specification layer
        /// </summary>
        /// <param name="path"></param>
        public static void Initialize(string path)
        {
            Path = path;

            // check file for existance
            if (!File.Exists(path))
            {
                throw  new Exception($"File {path} does not exist");
            }
        }

        /// <summary>
        /// Path of XML
        /// </summary>
        public static string Path { get; private set; }

        /// <summary>
        /// Initialize Language Definitions
        /// </summary>
        private void InitializationImplementation()
        {

        }
    }
}