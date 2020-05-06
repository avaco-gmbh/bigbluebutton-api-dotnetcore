using System.Collections.Generic;
using System.Xml.Serialization;

namespace Avaco.BigBlueButton.Api.Models.Response {
    /// <summary>
    /// This class models the response data from a getMeetingInfo request, it contains the fields defined in the class BasicResponse
    /// </summary>
    /// <inheritdoc/>
    public class GetMeetingInfoResponse : BasicResponse {
        /// <summary>
        /// The name of the meeting
        /// </summary>
        [XmlElement ("meetingName")]
        public string MeetingName { get; set; }

        /// <summary>
        /// The meeting ID 
        /// </summary>
        [XmlElement ("meetingID")]
        public string MeetingID { get; set; }
        
        /// <summary>
        /// The internal meeting ID
        /// </summary>
        [XmlElement ("internalMeetingID")]
        public string InternalMeetingID { get; set; }

        /// <summary>
        /// The creation timestamp of the meeting
        /// </summary>
        [XmlElement ("createTime")]
        public long CreateTime { get; set; }

        /// <summary>
        /// The creation date of the meeting
        /// </summary>
        [XmlElement ("createDate")]
        public string CreateDate { get; set; }

        /// <summary>
        /// The voice bridge parameter provided on the creation call
        /// </summary>
        [XmlElement ("voiceBridge")]
        public string VoiceBridge { get; set; }

        /// <summary>
        /// The dial in phone number provided on the creation call
        /// </summary>
        [XmlElement ("dialNumber")]
        public string DialNumber { get; set; }

        /// <summary>
        /// The attendee password
        /// </summary>
        [XmlElement ("attendeePW")]
        public string AttendeePW { get; set; }

        /// <summary>
        /// The moderator password
        /// </summary>
        [XmlElement ("moderatorPW")]
        public string ModeratorPW { get; set; }

        /// <summary>
        /// An indicator if the meeting is currently running
        /// </summary>
        [XmlElement ("running")]
        public bool Running { get; set; }

        /// <summary>
        /// The duration of the meeting in minutes
        /// </summary>
        [XmlElement ("duration")]
        public long Duration { get; set; }

        /// <summary>
        /// An indicator if any user has joined the meeting
        /// </summary>
        [XmlElement ("hasUserJoined")]
        public bool HasUserJoined { get; set; }

        /// <summary>
        /// An indicator if the meeting is currently recording
        /// </summary>
        [XmlElement ("recording")]
        public bool Recording { get; set; }

        /// <summary>
        /// An indicator if the meeting was ended by a end api call
        /// </summary>
        [XmlElement ("hasBeenForciblyEnded")]
        public bool HasBeenForciblyEnded { get; set; }

        /// <summary>
        /// The start time of the meeting
        /// </summary>
        [XmlElement ("startTime")]
        public long StartTime { get; set; }

        /// <summary>
        /// The end time of the meeting
        /// </summary>
        [XmlElement ("endTime")]
        public long EndTime { get; set; }

        /// <summary>
        /// The count of participants that take/took part at the meeting
        /// </summary>
        [XmlElement ("participantCount")]
        public long ParticipantCount { get; set; }

        /// <summary>
        /// The amount of participants in listening mode
        /// </summary>
        [XmlElement ("listenerCount")]
        public long ListenerCount { get; set; }

        /// <summary>
        /// The amount of participants with voice enabled
        /// </summary>
        [XmlElement ("voiceParticipantCount")]
        public long VoiceParticipantCount { get; set; }

        /// <summary>
        /// The amount amount of participants with video enabled
        /// </summary>
        [XmlElement ("videoCount")]
        public long VideoCount { get; set; }

        /// <summary>
        /// The user limit for the meeting
        /// </summary>
        [XmlElement ("maxUsers")]
        public long MaxUsers { get; set; }

        /// <summary>
        /// The amount of moderators for the meeting
        /// </summary>
        [XmlElement ("moderatorCount")]
        public long ModeratorCount { get; set; }

        /// <summary>
        /// The list of attendees for the meeting
        /// </summary>
        [XmlElement ("attendees")]
        public List<Attendee> Attendees { get; set; }
    }

    /// <summary>
    /// This class models a meeting attendee 
    /// </summary>
    public class Attendee {
        /// <summary>
        /// The userID of the attendee (this one can be provided by the join call)
        /// </summary>
        [XmlElement ("userID")]
        public string UserId { get; set; }

        /// <summary>
        /// The name of the attendee (shown in the client)
        /// </summary>
        [XmlElement ("fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// The attendee role
        /// </summary>
        [XmlElement ("role")]
        public string Role { get; set; }

        /// <summary>
        /// An indicator if the attendee is in presenter mode
        /// </summary>
        [XmlElement ("isPresenter")]
        public bool IsPresenter { get; set; }

        /// <summary>
        /// An indicator if the attendee is in listening only mode
        /// </summary>
        [XmlElement ("isListeningOnly")]
        public bool IsListeningOnly { get; set; }

        /// <summary>
        /// An indicator if the attendee has voice enabled
        /// </summary>
        [XmlElement ("hasJoinedVoice")]
        public bool HasJoinedVoice { get; set; }
        
        /// <summary>
        /// An indicator if the attendee has video enabled
        /// </summary>
        [XmlElement ("hasVideo")]
        public bool HasVideo { get; set; }

        /// <summary>
        /// An indicator wich client the attendee used to join 
        /// </summary>
        [XmlElement ("clientType")]
        public string ClientType { get; set; }
    }
}