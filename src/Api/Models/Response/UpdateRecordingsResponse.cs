namespace Avaco.BigBlueButton.Api.Models.Response 
{
    /// <summary>
    /// This class models the response data from a updateRecording request, it contains details about the newly created meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class UpdateRecordingsResponse: BasicResponse
    {
        /// <summary>
        /// An indicator if the meeting was updated or not
        /// </summary>
        public bool Updated {get; set;}   
    }
}