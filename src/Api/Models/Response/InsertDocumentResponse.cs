using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Response {
    /// <summary>
    /// This class models the response data from an insertDocument request (BBB 3.x).
    /// </summary>
    /// <inheritdoc/>
    [XmlRoot("response")]
    public class InsertDocumentResponse : BasicResponse {
    }
}
