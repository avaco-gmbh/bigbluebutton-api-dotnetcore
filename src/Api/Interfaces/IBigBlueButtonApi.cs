using System;
using System.Threading.Tasks;
using Avaco.BigBlueButton.Api.Models.Request;
using Avaco.BigBlueButton.Api.Models.Response;
using Avaco.BigBlueButton.Rest;

namespace Avaco.BigBlueButton.Api.Interfaces {
    /// <summary>
    /// This Interface defines all methods that are necessary to communicate with a big blue button server.
    /// http://docs.bigbluebutton.org/dev/api.html 
    /// </summary>
    public interface IBigBlueButtonApi {
        /// <summary>
        /// This method creates a new meeting room using a limited amount of parameters
        /// </summary>
        /// <param name="meetingID"> The meeting ID used to reference the meeting in subsequent calls (a good choice is a UUID) </param>
        /// <param name="name"> The name of the meeting room </param>
        /// <param name="attendeePW"> The password for attendees to join the meeting </param>
        /// <param name="moderatorPW"> The password for moderators to join the meeting </param>
        /// <param name="welcome"> A welcome message to be shown on the meeting room </param>
        /// <param name="requestBody"> A Request body containing data for preuploaded slides </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<CreateResponse>> CreateAsync ( string meetingID,string name = null,string attendeePW = null,string moderatorPW = null,string welcome = null, CreateRequest requestBody = null);

        /// <summary>
        /// This method creates a new meeting room
        /// </summary>
        /// <param name="meetingID"> The meeting ID used to reference the meeting in subsequent calls (a good choice is a UUID) </param>
        /// <param name="name"> The name of the meeting room </param>
        /// <param name="attendeePW"> The password for attendees to join the meeting </param>
        /// <param name="moderatorPW"> The password for moderators to join the meeting </param>
        /// <param name="welcome"> A welcome message to be shown on the meeting room </param>
        /// <param name="dialNumber"> A dial number users can call to join by phone</param>
        /// <param name="voiceBridge"> A PIN number for phone users</param>
        /// <param name="maxParticipants"> The maximum amount of participants for the meeting</param>
        /// <param name="logoutURL"> A url used for redirection after logout </param>
        /// <param name="record"> An indicator if the meeting should be recorded </param>
        /// <param name="duration"> The maximum length (in minutes) for the meeting </param>
        /// <param name="isBreakout"> Must be set to true to create a breakout room </param>
        /// <param name="parentMeetingID"> Must be provided when creating a breakout room, the parent room must be running </param>
        /// <param name="sequence"> The sequence number of the breakout room </param>
        /// <param name="freeJoin"> If set to true, the client will give the user the choice to choose the breakout rooms he wants to join </param>
        /// <param name="moderatorOnlyMessage"> You can pass one or more metadata values when creating a meeting. These will be stored by BigBlueButton can be retrieved later via the getMeetingInfo and getRecordings calls </param>
        /// <param name="autoStartRecording"> Whether to automatically start recording when first user joins </param>
        /// <param name="allowStartStopRecording"> Allow the user to start/stop recording </param>
        /// <param name="webcamsOnlyForModerator"> Setting webcamsOnlyForModerator=true will cause all webcams shared by viewers during this meeting to only appear for moderators </param>
        /// <param name="logo"> Setting logo=http://www.example.com/my-custom-logo.png will replace the default logo in the Flash client </param>
        /// <param name="bannerText"> Will set the banner text in the client </param>
        /// <param name="bannerColor"> Will set the banner background color in the client (Hex format: 0xffffff)</param>
        /// <param name="copyright"> Setting copyright=My custom copyright will replace the default copyright on the footer of the Flash client </param>
        /// <param name="muteOnStart"> Setting muteOnStart=true will mute all users when the meeting starts </param>
        /// <param name="allowModsToUnmuteUsers"> Setting to allowModsToUnmuteUsers=true will allow moderators to unmute other users in the meeting </param>
        /// <param name="lockSettingsDisableCam"> Setting lockSettingsDisableCam=true will prevent users from sharing their camera in the meeting </param>
        /// <param name="lockSettingsDisableMic"> Setting to lockSettingsDisableMic=true will only allow user to join listen only </param>
        /// <param name="lockSettingsDisablePrivateChat"> Setting to lockSettingsDisablePrivateChat=true will disable private chats in the meeting </param>
        /// <param name="lockSettingsDisablePublicChat"> Setting to lockSettingsDisablePublicChat=true will disable public chat in the meeting </param>
        /// <param name="lockSettingsDisableNote"> Setting to lockSettingsDisableNote=true will disable notes in the meeting </param>
        /// <param name="lockSettingsLockedLayout"> Setting to lockSettingsLockedLayout=true will lock the layout in the meeting </param>
        /// <param name="lockSettingsLockOnJoin"> Setting to lockSettingsLockOnJoin=false will not apply lock setting to users when they join </param>
        /// <param name="lockSettingsLockOnJoinConfigurable"> Setting to lockSettingsLockOnJoinConfigurable=true will allow applying of lockSettingsLockOnJoin param </param>
        /// <param name="guestPolicy"> Default guestPolicy=ALWAYS_ACCEPT. Will set the guest policy for the meeting. The guest policy determines whether or not users who send a join request with guest=true will be allowed to join the meeting. Possible values are ALWAYS_ACCEPT, ALWAYS_DENY, and ASK_MODERATOR</param>
        /// <param name="meta"> You can pass one or more metadata values when creating a meeting. These will be stored by BigBlueButton can be retrieved later via the getMeetingInfo and getRecordings calls </param>
        /// <param name="allowDuplicateExtUserid">Setting to allowDuplicateExtUserid=false will not allow user to join into a meeting from multiple devices or browser tabs</parm>
        /// <param name="requestBody"> A Request body containing data for preuploaded slides </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<CreateResponse>> CreateAsync (
            string meetingID,
            string name = null,
            string attendeePW = null,
            string moderatorPW = null,
            string welcome = null,          
            string dialNumber = null,
            string voiceBridge = null,
            long? maxParticipants = null,
            string logoutURL = null,
            bool record = false,
            long? duration = null,
            bool? isBreakout = null,
            string parentMeetingID = null,
            long? sequence = null,
            bool? freeJoin = null,
            string moderatorOnlyMessage = null,
            bool autoStartRecording = false,
            bool allowStartStopRecording = false,
            bool? webcamsOnlyForModerator = null,
            string logo = null,
            string bannerText = null,
            string bannerColor = null,
            string copyright = null,
            bool muteOnStart = true,
            bool allowModsToUnmuteUsers = false,
            bool lockSettingsDisableCam = false,
            bool lockSettingsDisableMic = false,
            bool lockSettingsDisablePrivateChat = false,
            bool lockSettingsDisablePublicChat = false,
            bool lockSettingsDisableNote = false,
            bool lockSettingsLockedLayout = false,
            bool lockSettingsLockOnJoin = true,
            bool lockSettingsLockOnJoinConfigurable = false,
            string guestPolicy = "ALWAYS_ACCEPT",
            string[] meta = null,
            bool allowDuplicateExtUserid = true,
            CreateRequest requestBody = null
        );

        /// <summary>
        /// A function to join a meeting 
        /// </summary>
        /// <param name="fullName"> The full name that is to be used to identify this user </param>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to join </param>
        /// <param name="password"> The password used to authenticate as moderator or attendee </param>
        /// <param name="createTime"> BigBlueButton will ensure it matches the ‘createTime’ for the session </param>
        /// <param name="userID"> An identifier for this user that will help your application to identify which person this is </param>
        /// <param name="webVoiceConfig"> If you want to pass in a custom voice-extension when a user joins the voice conference using voip </param>
        /// <param name="configToken"> The token returned by a setConfigXML API call </param>
        /// <param name="defaultLayout"> The layout name to be loaded first when the application is loaded </param>
        /// <param name="avatarURL"> The layout name to be loaded first when the application is loaded </param>
        /// <param name="redirect"> The default behaviour of the JOIN API is to redirect the browser to the Flash client when the JOIN call succeeds </param>
        /// <param name="clientURL">Some third party apps what to display their own custom client. These apps can pass the URL containing the custom client and when redirect is not set to false, the browser will get redirected to the value of clientURL</param>
        /// <param name="joinViaHtml5">Set to “true” to force the HTML5 client to load for the user</param>
        /// <param name="guest">Set to “true” to indicate that the user is a guest</param>
        /// <param name="userdata"> A string[] containing parameters for customization </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<JoinResponse>> JoinAsync (
            string fullName,
            string meetingID,
            string password,
            string createTime = null,
            string userID = null,
            string webVoiceConfig = null,
            string configToken = null,
            string defaultLayout = null,
            string avatarURL = null,
            string redirect = "false",
            string clientURL = null,
            string joinViaHtml5 = "true",
            string guest = "true",
            string[] userdata = null
        );

        /// <summary>
        /// A function to join a meeting 
        /// </summary>
        /// <param name="fullName"> The full name that is to be used to identify this user </param>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to join </param>
        /// <param name="password"> The password used to authenticate as moderator or attendee </param>
        /// <param name="createTime"> BigBlueButton will ensure it matches the ‘createTime’ for the session </param>
        /// <param name="userID"> An identifier for this user that will help your application to identify which person this is </param>
        /// <param name="webVoiceConfig"> If you want to pass in a custom voice-extension when a user joins the voice conference using voip </param>
        /// <param name="configToken"> The token returned by a setConfigXML API call </param>
        /// <param name="defaultLayout"> The layout name to be loaded first when the application is loaded </param>
        /// <param name="avatarURL"> The layout name to be loaded first when the application is loaded </param>
        /// <param name="redirect"> The default behaviour of the JOIN API is to redirect the browser to the Flash client when the JOIN call succeeds </param>
        /// <param name="clientURL">Some third party apps what to display their own custom client. These apps can pass the URL containing the custom client and when redirect is not set to false, the browser will get redirected to the value of clientURL</param>
        /// <param name="joinViaHtml5">Set to “true” to force the HTML5 client to load for the user</param>
        /// <param name="guest">Set to “true” to indicate that the user is a guest</param>
        /// <param name="userdata"> A string[] containing parameters for customization </param>
        /// <returns> The URI to join the meeting </returns>
        Uri JoinUri (
            string fullName,
            string meetingID,
            string password = null,
            string createTime = null,
            string userID = null,
            string webVoiceConfig = null,
            string configToken = null,
            string defaultLayout = null,
            string avatarURL = null,
            string redirect = "false",
            string clientURL = null,
            string joinViaHtml5 = "true",
            string guest = "true",
            string[] userdata = null
        );

        /// <summary>
        /// A function to check if the meeting with the given meeting ID is currently running
        /// </summary>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to check on </param>
        /// <see cref="IsMeetingRunningResponse"/> for details on the returned data
        /// <returns> A response object containing the HTTP status code as well as the returned data  </returns>
        Task<RestApiResponse<IsMeetingRunningResponse>> IsMeetingRunningAsync (string meetingID);

        /// <summary>
        /// A function to end a meeting, the password used needs to be a moderator password
        /// </summary>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to end </param>
        /// <param name="password"> The moderator password for this meeting. You can not end a meeting using the attendee password </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<EndResponse>> EndAsync (string meetingID, string password);

        /// <summary>
        /// A function to get meeting details
        /// </summary>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to check on </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<GetMeetingInfoResponse>> GetMeetingInfoAsync (string meetingID);

        /// <summary>
        /// This call will return a list of all the meetings found on this server
        /// </summary>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<GetMeetingsResponse>> GetMeetingsAsync ();

        /// <summary>
        /// Retrieves the recordings that are available for playback for a given meetingID (or set of meeting IDs)
        /// </summary>
        /// <param name="meetingID"> A meeting ID for get the recordings. It can be a set of meetingIDs separate by commas. If the meeting ID is not specified, it will get ALL the recordings. If a recordID is specified, the meetingID is ignored </param>
        /// <param name="recordID"> A record ID for get the recordings. It can be a set of recordIDs separate by commas. If the record ID is not specified, it will use meeting ID as the main criteria. If neither the meeting ID is specified, it will get ALL the recordings. The recordID can also be used as a wildcard by including only the first characters in the string </param>
        /// <param name="state"> The parameter state can be used to filter results. It can be a set of states separate by commas. If it is not specified only the states [published|unpublished] are considered (same as in previous versions). If it is specified as “any”, recordings in all states are included </param>
        /// <param name="meta"> You can pass one or more metadata values to filter the recordings returned. The format of these parameters is the same as the metadata passed to the create call </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<GetRecordingsResponse>> GetRecordingsAsync(string meetingID = null, string recordID = null, string state = null, string[] meta = null);
        
        /// <summary>
        /// Publish and unpublish recordings for a given recordID (or set of record IDs)
        /// </summary>
        /// <param name="recordID"> A record ID for specify the recordings to apply the publish action. It can be a set of record IDs separated by commas </param>
        /// <param name="publish"> The value for publish or unpublish the recording(s). Available values: true or false </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<PublishRecordingsResponse>> PublishRecordingsAsync(string recordID, bool publish);

        /// <summary>
        /// Delete one or more recordings for a given recordID (or set of record IDs)
        /// </summary>
        /// <param name="recordID">A record ID for specify the recordings to delete. It can be a set of record IDs separated by commas</param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<DeleteRecordingsResponse>> DeleteRecordingAsync(string recordID);

        /// <summary>
        /// Update metadata for a given recordID (or set of record IDs)
        /// </summary>
        /// <param name="recordID"> A record ID for specify the recordings to apply the publish action. It can be a set of record IDs separated by commas </param>
        /// <param name="meta"> You can pass one or more metadata values to be updated. The format of these parameters is the same as the metadata passed to the create call </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<UpdateRecordingsResponse>> UpdateRecordingsAsync(string recordID, string[] meta = null);

        /// <summary>
        /// Retrieve the default config.xml
        /// </summary>
        /// <returns> The config.xml as string</returns>
        Task<string> GetDefaultConfigXmlAsync();

        /// <summary>
        /// Associate a custom config.xml file with the current session
        /// </summary>
        /// <param name="meetingID"> A meetingID to an active meeting </param>
        /// <param name="configXML"> A valid config.xml file </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<string> SetDefaultConfigXmlAsync(string meetingID, string configXML);

        Task<string> GetRecordingTextTracksAsync(string recordID);
        Task<string> PutRecordingTextTrackAsync(string recordID,string kind, string lang, string label );
    }
}