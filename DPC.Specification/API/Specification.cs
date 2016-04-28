using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using DPC.Specification.Definitions;
using DPC.Specification.Repository;
using DPC.Specification.Utility;

namespace DPC.Specification.API
{
    /// <summary>
    /// Specification layer representative
    /// </summary>
    public class Specification
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static Specification()
        {
            Path = string.Empty;
            XSDPath = string.Empty;
            OperatorMapping = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initialize specification layer
        /// </summary>
        /// <param name="path"></param>
        public static void Initialize(string path)
        {
            // set xml path
            Path = path;

            // check file for existance
            if (!File.Exists(path))
            {
                throw  new Exception($"File {path} does not exist");
            }

            // initialize
            InitializeImplementation();
        }

        /// <summary>
        /// Path of XML
        /// </summary>
        public static string Path { get; private set; }

        /// <summary>
        /// Path of XSD
        /// </summary>
        public static string XSDPath { get; private set; }

        /// <summary>
        /// XML document to be stored here
        /// </summary>
        private static XmlDocument Document { get; set; }

        /// <summary>
        /// Mapping XSD based id of operator to XML based name
        /// </summary>
        private static Dictionary<string, string> OperatorMapping { get; } 

        /// <summary>
        /// Initialize Language Definitions
        /// </summary>
        private static void InitializeImplementation()
        {
            // read doc
            ReadDocument();

            // try get XSD file
            ResolveXSD();

            // Initialize XSD
            InitializeXsd();

            // Initialize XML
            InitializeXml();
        }

        /// <summary>
        /// Initialize XML related data
        /// </summary>
        private static void InitializeXml()
        {
            // get language definitions node
            var languageDefinitions = Document.DocumentElement;

            // check for element presence
            if (languageDefinitions == null)
            {
                throw  new Exception($"Something went wrong in document {Path}");
            }

            // iterate over laguage definitions
            foreach (XmlNode languageDefinitionNode in languageDefinitions.ChildNodes)
            {
                // check for existance
                if (languageDefinitionNode.Attributes?["Name"] == null)
                {
                    throw new Exception("Unable to read name of language");
                }

                // name of language
                var name = languageDefinitionNode.Attributes?["Name"].InnerText;

                // create language
                var language = new LanguageDefinition()
                {
                    Name = name
                };

                // get metadata node
                var metadataNode = languageDefinitionNode["MetadataDefinition"];

                // try fill metadata information
                if (metadataNode != null)
                {
                    // try get Open and Close brackets
                    language.Metadata.OpenBracket = metadataNode.Attributes["OpenBracket"]?.InnerText ?? string.Empty;
                    language.Metadata.CloseBracket = metadataNode.Attributes["CloseBracket"]?.InnerText ?? string.Empty;

                    // default prioritizer
                    PrioritizerOption prioritizer;

                    // try parse prioritizer option
                    Enum.TryParse(metadataNode.Attributes["Prioritizer"]?.InnerText ?? string.Empty, true, out prioritizer);

                    // then assign
                    language.Metadata.Prioritizer = prioritizer;
                }

                // get predicates node
                var predicatesNode = languageDefinitionNode["PredicateDefinitions"];
                var logicalsNode = languageDefinitionNode["LogicalOperatorDefinitions"];

                // check
                if (predicatesNode == null || logicalsNode == null)
                {
                    throw  new Exception("Predicates or Logicals node is not defined");
                }

                // iterate over predicate definitions
                foreach (XmlNode predNode in predicatesNode.ChildNodes )
                {
                    // parse predicate into language
                    ParsePredicateNode(predNode, language);
                }

                // iterate over logical definitions
                foreach (XmlNode logicalNode in logicalsNode.ChildNodes)
                {
                    // parse logical operator into language
                    ParseLogicalOperationNode(logicalNode, language);
                }


                // register corresponding laguage definition
                LanguageDefinitionRepository
                    .Instance
                    .RegisterLanguageDefinition(language);                
            }
        }

        /// <summary>
        /// Parse logical operator node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="language"></param>
        private static void ParseLogicalOperationNode(XmlNode node, LanguageDefinition language)
        {
            // get opcode
            var opcode = node.Attributes?["Operator"]?.InnerText;

            // get implementation
            var implentation = node.Attributes?["Implementation"]?.InnerText;

            // try get dimention
            var dimention = int.Parse(node.Attributes?["Dimention"]?.InnerText ?? "-1");

            // check of existence
            if (opcode == null || implentation == null)
            {
                throw  new Exception($"Operator name {opcode ?? string.Empty} and implementation {implentation ?? string.Empty} should be set");
            }

            // add logical operator
            language.AddLogicalOperator(new LogicalOperatorDefinition()
            {
                Operator = opcode,
                Implementation = implentation,
                Dimention =  (dimention == -1) ? implentation.CountTempaltedArgs() : dimention
            });
        }

        /// <summary>
        /// Parse predicate operator node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="language"></param>
        private static void ParsePredicateNode(XmlNode node, LanguageDefinition language)
        {
            // get opcode
            var opcode = node.Attributes?["Operator"]?.InnerText;

            // get implementation
            var implentation = node.Attributes?["Implementation"]?.InnerText;

            // try get dimention
            var dimention = int.Parse(node.Attributes?["Dimention"]?.InnerText ?? "-1");

            // check of existence
            if (opcode == null || implentation == null)
            {
                throw new Exception($"Operator name {opcode ?? string.Empty} and implementation {implentation ?? string.Empty} should be set");
            }

            // add logical operator
            language.AddPredicateOperator(new PredicateOperatorDefinition()
            {
                Operator = opcode,
                Implementation = implentation,
                Dimention = (dimention == -1) ? implentation.CountTempaltedArgs() : dimention
            });
        }

        /// <summary>
        /// Perform initialization part based on XSD
        /// </summary>
        private static void InitializeXsd()
        {
            // create schema set
            var schemaSet = new XmlSchemaSet();

            // ns URI
            var ns = string.Empty;
            
            // check for nulls
            if (Document.DocumentElement != null)
            {
                ns = Document.DocumentElement.NamespaceURI;
            }

            // add schema into set
            if (Document.DocumentElement != null)
            {
                schemaSet.Add(ns, XSDPath);
            }

            // then compile
            schemaSet.Compile();

            // prepare schema
            var schema = default(XmlSchema);

            // get last compiled schema 
            foreach (XmlSchema currentSchema in schemaSet.Schemas())
            {
                schema = currentSchema;
            }

            // check schema
            if (schema == null)
            {
                throw  new Exception($"Something is wrong with schema in XSD {XSDPath}");
            }

            // get logical type restiction
            var logicalTypeRestrictionType = schema
                .SchemaTypes[new XmlQualifiedName("LogicalOperatorType", ns)] as XmlSchemaSimpleType;

            // get predicate restriction
            var predicateTypeRestrictionType = schema
                .SchemaTypes[new XmlQualifiedName("PredicateType", ns)] as XmlSchemaSimpleType;


            // check restrictoins
            if (logicalTypeRestrictionType == null || predicateTypeRestrictionType == null)
            {
                throw new Exception($"Something is wrong with schema in XSD {XSDPath}");
            }

            // get logicals and predicates
            var logicals = logicalTypeRestrictionType.Content as XmlSchemaSimpleTypeRestriction;
            var predicates = predicateTypeRestrictionType.Content as XmlSchemaSimpleTypeRestriction;

            // check restrictoins
            if (logicals == null || predicates == null)
            {
                throw new Exception($"Something is wrong with schema in XSD {XSDPath}");
            }

            // get all facets
            var facets = logicals
                .Facets
                .Cast<XmlSchemaEnumerationFacet>()
                .Union(predicates.Facets.Cast<XmlSchemaEnumerationFacet>());
            
            // iterate on logical facet
            foreach (var facet in facets)
            {
                // check existance
                if (OperatorMapping.ContainsKey(facet.Id))
                {
                    throw  new Exception($"Operation mapping with key {facet.Id} is already added");
                }

                // insert operation mapping
                OperatorMapping.Add(facet.Id, facet.Value);
            }
        }

        /// <summary>
        /// Resolve full path of xsd and write into XSDPath
        /// </summary>
        private static void ResolveXSD()
        {
            // full path of xml directory
            var fullPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(Path)) ?? string.Empty;

            // relative path of xsd
            var xsdRelativePath = string.Empty;

            // check for parsing errors
            if (Document?.DocumentElement == null)
            {
                throw new Exception($"Something went wrong while parsing {Path}");
            }

            // relative path of xsd schema
            var schemaLocationNode = Document
                .DocumentElement
                .SelectSingleNode("//@*[local-name()='schemaLocation']");

            // try to get schema location relative path
            if (schemaLocationNode != null)
            {
                xsdRelativePath = schemaLocationNode.Value.Split(null)[1];
            }

            // assign XSD path
            XSDPath = System.IO.Path.Combine(fullPath, xsdRelativePath);

            // check for xsd file
            if (!File.Exists(XSDPath))
            {
                throw new Exception($"Unable to locate XSD file {XSDPath}");
            }
        }

        /// <summary>
        /// Read XML document into memory
        /// </summary>
        private static void ReadDocument()
        {
            // prepare doc
            var document = new XmlDocument();

            // prepare stream reader
            using (var reader = new StreamReader(Path))
            {
                // read stream into document
                document.Load(reader);
            }

            // assign document
            Document = document;
        }

        /// <summary>
        /// Map opcode to XML based name
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public static string MapOpCode(string opcode)
        {
            if (!OperatorMapping.ContainsKey(opcode))
            {
                throw new Exception($"Operator {opcode} is not supported by XSD {XSDPath}");
            }

            return OperatorMapping[opcode];
        }
    }
}