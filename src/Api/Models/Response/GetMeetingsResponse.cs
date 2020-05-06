using System.Collections.Generic;
namespace Avaco.BigBlueButton.Api.Models.Response
{

    /// <summary>
    /// This class models the response data from a getMeetings request, it contains details about the newly created meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class GetMeetingsResponse : BasicResponse
    {  
        /// <summary>
        /// A list of all active meetings 
        /// </summary>
        public List<GetMeetingInfoResponse> Meetings {get;set;}
    }
}