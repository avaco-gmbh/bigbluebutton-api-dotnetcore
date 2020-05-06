using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Response
{
    /// <summary>
    /// This class models the response data from a isMeeting request, it contains details about the newly created meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class IsMeetingRunningResponse : BasicResponse
    {
        /// <summary>
        /// An indicator if the meeting is currently running
        /// </summary>
        [XmlElement("running")]
        public bool Running {get;set;}
    }
}