using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Request
{   

    /// <summary>
    /// This class models a request body for a create request.
    /// The serialized xml data will look like this:
    /// <c>
    /// <modules>
    ///    <module name="presentation">
    ///       <document url="http://www.sample-pdf.com/sample.pdf" filename="report.pdf"/>
    ///       <document name="sample-presentation.pdf">JVBERi0xLjQKJ....
    ///         [clipped here]
    ///         ....0CiUlRU9GCg==
    ///       </document>
    ///    </module>
    /// </modules>
    /// </c>
    /// </summary>
    [XmlRoot("modules")]
    public class CreateRequest
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CreateRequest(){
            // Create the default module for presentations
            Modules.Add(new Module{ Name="presentation"});
        }

        /// <summary>
        /// Add a new document using a base64 string
        /// </summary>
        /// <param name="module"> The name of the module that is supposed to contain the document</param>
        /// <param name="name"> The name of the file </param>
        /// <param name="base64Value"> The file encoded as base64 string </param>
        public void AddDocumentByData(string module, string name, string base64Value){
            AddDocument(module, Document.CreateByData(name, base64Value));
        }

        /// <summary>
        /// Add a new document using a url as source 
        /// </summary>
        /// <param name="module"> The name of the module that is supposed to contain the document</param>
        /// <param name="url"> The url where the file is located </param>
        /// <param name="filename"> The filename used inside the big blue button server</param>
        public void AddDocumentByUrl(string module, string url, string filename){
            AddDocument(module, Document.CreateByUrl(url, filename));
        }

        /// <summary>
        /// Add a new document
        /// </summary>
        /// <param name="module"> The name of the module that is supposed to contain the document</param>
        /// <param name="document"> The document to add </param>
        public void AddDocument(string module, Document document){
            Modules.FirstOrDefault(m => m.Name == module)?.Documents.Add(document);

        }

        /// <summary>
        /// A list of modules that is supposed to be transmitted to the server
        /// </summary>
        /// <typeparam name="Module">The module type</typeparam>
        [XmlElement("module")]
        public List<Module> Modules {get;set;} = new List<Module>();
    }

    /// <summary>
    /// The module representation
    /// <c>
    ///    <module name="presentation">
    ///       <document url="http://www.sample-pdf.com/sample.pdf" filename="report.pdf"/>
    ///    </module>
    /// </c>
    /// </summary>
    public class Module {
        /// <summary>
        /// The name attribute of the module tag
        /// </summary>
        [XmlAttribute("name")]
        public string Name {get; set;}

        /// <summary>
        /// The list of document tags contained by the module
        /// </summary>
        /// <typeparam name="Document"> The document representation </typeparam>
        [XmlElement("document")]
        public List<Document> Documents {get;set;} = new List<Document>();
    }

    /// <summary>
    /// The document representation
    /// <c>
    ///     <document url="http://www.sample-pdf.com/sample.pdf" filename="report.pdf"/>
    /// </c>
    /// </summary>
    public class Document {
        /// <summary>
        /// A factory method to create a document based on base64 data
        /// </summary>
        /// <param name="name"> The filename </param>
        /// <param name="data"> The base64 string containing the file data</param>
        /// <returns> The newly created document object </returns>
        public static Document CreateByData(string name, string data){
            return new Document {Name = name, Value=data};
        }

        /// <summary>
        /// A factory method to create a document based on url
        /// </summary>
        /// <param name="url"> The url where the file is located </param>
        /// <param name="filename"> The filename</param>
        /// <returns> The newly created document object </returns>
        public static Document CreateByUrl(string url, string filename){
            return new Document {Url = url, Filename = filename };
        }

        /// <summary>
        /// The url xml attribute of the document tag
        /// </summary>
        [XmlAttribute("url")]
        public string Url {get; set;}

        /// <summary>
        /// The filename xml attribute of the document tag
        /// </summary>
        [XmlAttribute("filename")]
        public string Filename {get; set;}

        /// <summary>
        /// The name xml attribute of the document tag
        /// </summary>
        [XmlAttribute("name")]
        public string Name {get; set;}

        /// <summary>
        /// The tag value of the document tag
        /// </summary>
        [XmlText]
        public string Value {get;set;}
    }
}