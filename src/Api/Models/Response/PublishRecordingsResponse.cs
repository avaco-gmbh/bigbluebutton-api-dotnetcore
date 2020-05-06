namespace Avaco.BigBlueButton.Api.Models.Response 
{
    /// <summary>
    /// This class models the response data from a publishRecording request, it contains details about the newly created meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class PublishRecordingsResponse : BasicResponse
    {
        /// <summary>
        /// An indicator if the meeting was published or not
        /// </summary>
        public bool Published {get; set;}
    }
}