using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Response {
    /// <summary>
    /// This class models the response data from a join request, it contains details about the newly created meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class JoinResponse : BasicResponse
    {
        /// <summary>
        /// The meeting ID of the meeting that was requested to join 
        /// </summary>
        [XmlElement("meeting_id")]
        public string MeetingID {get;set;}

        /// <summary>
        /// The user ID that was assigned to the user
        /// </summary>
        [XmlElement("user_id")]
        public string UserID {get;set;}

        /// <summary>
        /// The auth token issued by the server
        /// </summary>
        [XmlElement("auth_token")]
        public string AuthToken {get;set;}

        /// <summary>
        /// The session token used to join the meeting
        /// </summary>
        [XmlElement("session_token")]
        public string SessionToken {get;set;}

        /// <summary>
        /// A url that is used to join the meeting
        /// </summary>
        [XmlElement("url")]
        public string Url {get;set;}
    
    }
}