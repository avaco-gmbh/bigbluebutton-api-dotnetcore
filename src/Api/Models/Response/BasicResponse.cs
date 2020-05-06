
using System;
using System.Net;
using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Response {
    /// <summary>
    /// The class that is used to deserialize the basic response fields received from the big blue button server
    /// </summary>
    [XmlRoot("response")]
    public class BasicResponse {
        /// <summary>
        /// The return code  SUCCESS or FAILED
        /// </summary>
        [XmlElement("returnCode")]
        public string ReturnCode {get;set;}

        /// <summary>
        /// This field contains a message key used to correlate the message
        /// </summary>
        [XmlElement("messageKey")]
        public string MessageKey {get;set;}

        /// <summary>
        /// This field contains a message
        /// </summary>
        [XmlElement("message")]
        public string Message {get;set;}
    }
}