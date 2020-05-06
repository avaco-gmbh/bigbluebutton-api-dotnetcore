using System.Collections.Generic;
using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Response {
    /// <summary>
    /// This class models the response data from a getRecordings request, it contains details about the newly created meeting as well
    /// as the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class GetRecordingsResponse : BasicResponse {
        /// <summary>
        /// A list of all returned recordings
        /// </summary>
        public List<Recording> Recordings { get; set; }
    }

    /// <summary>
    /// This class models the details of a recording info
    /// </summary>
    public class Recording {
        /// <summary>
        /// The id of the record
        /// </summary>
        public string RecordID { get; set; }

        /// <summary>
        /// The meeting ID of the meeting that was recorded
        /// </summary>
        public string MeetingID { get; set; }

        /// <summary>
        /// The internal meeting ID of the meeting that was recorded
        /// </summary>
        public string InternalMeetingID { get; set; }

        /// <summary>
        /// The name of the meeting that was recorded
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An indicator if the recorded meeting was a breakout room
        /// </summary>
        public bool IsBreakout { get; set; }

        /// <summary>
        /// The state of the recording (e.g. published or not)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The start time of the recording
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// The end time of the recording
        /// </summary>
        public long EndTime { get; set; }
        
        /// <summary>
        /// The amount of participants that took part on the recording
        /// </summary>
        public long Participants { get; set; }

        //TODO: Add metadata

        /// <summary>
        /// The list of playback formats available for the recording
        /// </summary>
        public List<Format> Playback { get; set; }
    }

    /// <summary>
    /// This class models a playback format of a recording
    /// </summary>
    public class Format {
        /// <summary>
        /// The type of the format (e.g. presentation or podcast)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The url where the recording can be accessed
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The amount of time taken to process the recording
        /// </summary>
        public long ProcessingTime { get; set; }

        /// <summary>
        /// The length of the recording
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// A preview for the recording
        /// </summary>
        public Preview Preview { get; set; }
    }

    /// <summary>
    /// This class models a recording preview
    /// </summary>
    public class Preview {

        /// <summary>
        /// A list of preview image data
        /// </summary>
        public List<ImageData> Images { get; set; }
    }

    /// <summary>
    /// This class models a preview image for a recording
    /// </summary>
    public class ImageData {
        /// <summary>
        /// An image description
        /// </summary>
        [XmlAttribute]
        public string Alt { get; set; }

        /// <summary>
        /// The image height
        /// </summary>
        [XmlAttribute]
        public long Height { get; set; }

        /// <summary>
        /// The image width
        /// </summary>
        [XmlAttribute]
        public long Width { get; set; }

        /// <summary>
        /// A url that points to the image
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }

}