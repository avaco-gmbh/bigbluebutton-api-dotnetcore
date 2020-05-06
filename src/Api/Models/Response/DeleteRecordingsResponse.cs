namespace Avaco.BigBlueButton.Api.Models.Response 
{
    /// <summary>
    /// This class models the response data from a delete recording request, it contains details about the deletion progress meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class DeleteRecordingsResponse : BasicResponse
    {
        /// <summary>
        /// The status if the recording was deleted
        /// </summary>
        public bool Deleted {get; set;}
    }
}