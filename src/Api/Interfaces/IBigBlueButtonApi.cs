using System;
using System.Threading.Tasks;
using Avaco.BigBlueButton.Api.Models.Request;
using Avaco.BigBlueButton.Api.Models.Response;
using Avaco.BigBlueButton.Rest;

namespace Avaco.BigBlueButton.Api.Interfaces {
    /// <summary>
    /// This Interface defines all methods that are necessary to communicate with a big blue button server.
    /// https://docs.bigbluebutton.org/development/api/
    /// Supports BBB API 2.x and 3.x.
    /// </summary>
    public interface IBigBlueButtonApi {
        /// <summary>
        /// This method creates a new meeting room using a limited amount of parameters
        /// </summary>
        /// <param name="meetingID"> The meeting ID used to reference the meeting in subsequent calls (a good choice is a UUID) </param>
        /// <param name="name"> The name of the meeting room </param>
        /// <param name="attendeePW"> The password for attendees to join the meeting (optional in BBB 3.x) </param>
        /// <param name="moderatorPW"> The password for moderators to join the meeting (optional in BBB 3.x) </param>
        /// <param name="welcome"> A welcome message to be shown on the meeting room </param>
        /// <param name="requestBody"> A Request body containing data for preuploaded slides </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<CreateResponse>> CreateAsync ( string meetingID,string name = null,string attendeePW = null,string moderatorPW = null,string welcome = null, CreateRequest requestBody = null);

        /// <summary>
        /// This method creates a new meeting room with all available parameters
        /// </summary>
        /// <param name="meetingID"> The meeting ID used to reference the meeting in subsequent calls (a good choice is a UUID) </param>
        /// <param name="name"> The name of the meeting room </param>
        /// <param name="attendeePW"> The password for attendees to join the meeting (optional in BBB 3.x) </param>
        /// <param name="moderatorPW"> The password for moderators to join the meeting (optional in BBB 3.x) </param>
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
        /// <param name="moderatorOnlyMessage"> Display a message to all moderators in the public chat </param>
        /// <param name="autoStartRecording"> Whether to automatically start recording when first user joins </param>
        /// <param name="allowStartStopRecording"> Allow the user to start/stop recording </param>
        /// <param name="webcamsOnlyForModerator"> Setting webcamsOnlyForModerator=true will cause all webcams shared by viewers during this meeting to only appear for moderators </param>
        /// <param name="logo"> Setting logo=http://www.example.com/my-custom-logo.png will replace the default logo </param>
        /// <param name="bannerText"> Will set the banner text in the client </param>
        /// <param name="bannerColor"> Will set the banner background color in the client (Hex format: 0xffffff)</param>
        /// <param name="copyright"> Setting copyright will replace the default copyright on the footer </param>
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
        /// <param name="guestPolicy"> Will set the guest policy for the meeting. Possible values are ALWAYS_ACCEPT, ALWAYS_DENY, and ASK_MODERATOR</param>
        /// <param name="meta"> You can pass one or more metadata values when creating a meeting </param>
        /// <param name="allowDuplicateExtUserid"> Setting to false will not allow user to join from multiple devices or browser tabs</param>
        /// <param name="meetingExpireWhenLastUserLeftInMinutes"> Number of minutes to wait after the last user leaves before ending the meeting </param>
        /// <param name="meetingLayout"> The default meeting layout </param>
        /// <param name="endWhenNoModerator"> Whether the meeting should end when no moderator is present (BBB 2.5+/3.x) </param>
        /// <param name="endWhenNoModeratorDelayInMinutes"> Minutes to wait before ending meeting when no moderator (BBB 2.5+/3.x) </param>
        /// <param name="meetingKeepEvents"> Save meeting events even if the meeting is not recorded (BBB 2.5+/3.x) </param>
        /// <param name="allowModsToEjectCameras"> Allow moderators to eject webcams of other users (BBB 2.5+/3.x) </param>
        /// <param name="meetingCameraCap"> Maximum number of webcams for the meeting (0 = unlimited) (BBB 2.5+/3.x) </param>
        /// <param name="userCameraCap"> Maximum number of webcams per user (BBB 2.5+/3.x) </param>
        /// <param name="meetingExpireIfNoUserJoinedInMinutes"> Minutes to wait before expiring meeting if no user joins (BBB 2.5+/3.x) </param>
        /// <param name="meetingEndedURL"> Callback URL when meeting ends (BBB 2.5+/3.x) </param>
        /// <param name="disabledFeatures"> Comma-separated list of features to disable (BBB 2.5+/3.x) </param>
        /// <param name="disabledFeaturesExclude"> Comma-separated list of features to re-enable from the disabled list (BBB 3.x) </param>
        /// <param name="notifyRecordingIsOn"> Notify users when recording is on (BBB 2.5+/3.x) </param>
        /// <param name="presentationUploadExternalUrl"> External URL for presentation upload (BBB 2.5+/3.x) </param>
        /// <param name="presentationUploadExternalDescription"> Description for external presentation upload (BBB 2.5+/3.x) </param>
        /// <param name="learningDashboardCleanupDelayInMinutes"> Delay in minutes before cleaning up learning dashboard data (BBB 2.5+/3.x) </param>
        /// <param name="recordFullDurationMedia"> Record full duration if meeting is recorded (BBB 2.5+/3.x) </param>
        /// <param name="lockSettingsHideUserList"> Hide user list from viewers (BBB 2.5+/3.x) </param>
        /// <param name="lockSettingsHideViewersCursor"> Hide viewers cursor from other viewers (BBB 2.5+/3.x) </param>
        /// <param name="lockSettingsHideViewersAnnotation"> Hide viewers annotation from other viewers (BBB 3.x) </param>
        /// <param name="groups"> JSON string defining user groups (BBB 3.x) </param>
        /// <param name="breakoutRoomsRecord"> Whether breakout rooms should be recorded (BBB 2.5+/3.x) </param>
        /// <param name="breakoutRoomsPrivateChatEnabled"> Whether private chat is enabled in breakout rooms (BBB 2.5+/3.x) </param>
        /// <param name="allowPromoteGuestToModerator"> Allow promoting guest users to moderators (BBB 3.x) </param>
        /// <param name="preUploadedPresentationOverrideDefault"> Override default presentation with pre-uploaded one (BBB 2.5+/3.x) </param>
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
            int meetingExpireWhenLastUserLeftInMinutes = 1,
            string meetingLayout = "SMART_LAYOUT",
            // BBB 2.5+ / 3.x parameters
            bool? endWhenNoModerator = null,
            int? endWhenNoModeratorDelayInMinutes = null,
            bool? meetingKeepEvents = null,
            bool? allowModsToEjectCameras = null,
            int? meetingCameraCap = null,
            int? userCameraCap = null,
            int? meetingExpireIfNoUserJoinedInMinutes = null,
            string meetingEndedURL = null,
            string disabledFeatures = null,
            string disabledFeaturesExclude = null,
            bool? notifyRecordingIsOn = null,
            string presentationUploadExternalUrl = null,
            string presentationUploadExternalDescription = null,
            int? learningDashboardCleanupDelayInMinutes = null,
            bool? recordFullDurationMedia = null,
            bool? lockSettingsHideUserList = null,
            bool? lockSettingsHideViewersCursor = null,
            bool? lockSettingsHideViewersAnnotation = null,
            string groups = null,
            bool? breakoutRoomsRecord = null,
            bool? breakoutRoomsPrivateChatEnabled = null,
            bool? allowPromoteGuestToModerator = null,
            bool? preUploadedPresentationOverrideDefault = null,
            CreateRequest requestBody = null
        );

        /// <summary>
        /// A function to join a meeting.
        /// In BBB 3.x, the 'role' parameter is the preferred way to assign user roles.
        /// The 'password' parameter is still supported for backward compatibility with BBB 2.x.
        /// Either 'password' or 'role' should be provided.
        /// </summary>
        /// <param name="fullName"> The full name that is to be used to identify this user </param>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to join </param>
        /// <param name="password"> The password used to authenticate as moderator or attendee (deprecated in BBB 3.x, use role instead) </param>
        /// <param name="role"> The role for the user: "MODERATOR" or "VIEWER" (BBB 3.x). Takes precedence over password-based role assignment. </param>
        /// <param name="createTime"> BigBlueButton will ensure it matches the 'createTime' for the session </param>
        /// <param name="userID"> An identifier for this user that will help your application to identify which person this is </param>
        /// <param name="webVoiceConfig"> If you want to pass in a custom voice-extension when a user joins the voice conference using voip </param>
        /// <param name="defaultLayout"> The layout name to be loaded first when the application is loaded </param>
        /// <param name="avatarURL"> The URL for the user's avatar </param>
        /// <param name="redirect"> Controls whether the browser is redirected to the HTML5 client </param>
        /// <param name="clientURL">Some third party apps want to display their own custom client</param>
        /// <param name="guest">Set to "true" to indicate that the user is a guest</param>
        /// <param name="enforceLayout"> The layout to enforce for this user (BBB 3.x) </param>
        /// <param name="excludeFromDashboard"> Set to "true" to exclude this user from the learning dashboard (BBB 3.x) </param>
        /// <param name="errorRedirectUrl"> URL to redirect to in case of an error (BBB 3.x) </param>
        /// <param name="userdata"> A string[] containing parameters for customization </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<JoinResponse>> JoinAsync (
            string fullName,
            string meetingID,
            string password = null,
            string role = null,
            string createTime = null,
            string userID = null,
            string webVoiceConfig = null,
            string defaultLayout = null,
            string avatarURL = null,
            string redirect = "false",
            string clientURL = null,
            string guest = "true",
            string enforceLayout = null,
            string excludeFromDashboard = null,
            string errorRedirectUrl = null,
            string[] userdata = null
        );

        /// <summary>
        /// A function to generate a join URI without executing the request.
        /// In BBB 3.x, the 'role' parameter is the preferred way to assign user roles.
        /// </summary>
        /// <param name="fullName"> The full name that is to be used to identify this user </param>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to join </param>
        /// <param name="password"> The password used to authenticate as moderator or attendee (deprecated in BBB 3.x) </param>
        /// <param name="role"> The role for the user: "MODERATOR" or "VIEWER" (BBB 3.x) </param>
        /// <param name="createTime"> BigBlueButton will ensure it matches the 'createTime' for the session </param>
        /// <param name="userID"> An identifier for this user </param>
        /// <param name="webVoiceConfig"> Custom voice-extension for VOIP </param>
        /// <param name="defaultLayout"> The layout name to be loaded first </param>
        /// <param name="avatarURL"> The URL for the user's avatar </param>
        /// <param name="redirect"> Controls whether the browser is redirected </param>
        /// <param name="clientURL"> Custom client URL </param>
        /// <param name="guest"> Set to "true" for guest users </param>
        /// <param name="enforceLayout"> The layout to enforce (BBB 3.x) </param>
        /// <param name="excludeFromDashboard"> Exclude from learning dashboard (BBB 3.x) </param>
        /// <param name="errorRedirectUrl"> Error redirect URL (BBB 3.x) </param>
        /// <param name="userdata"> A string[] containing parameters for customization </param>
        /// <returns> The URI to join the meeting </returns>
        Uri JoinUri (
            string fullName,
            string meetingID,
            string password = null,
            string role = null,
            string createTime = null,
            string userID = null,
            string webVoiceConfig = null,
            string defaultLayout = null,
            string avatarURL = null,
            string redirect = "false",
            string clientURL = null,
            string guest = "true",
            string enforceLayout = null,
            string excludeFromDashboard = null,
            string errorRedirectUrl = null,
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
        /// A function to end a meeting. In BBB 3.x, authentication is done via the checksum (shared secret),
        /// so the password parameter is optional. For BBB 2.x compatibility, provide the moderator password.
        /// </summary>
        /// <param name="meetingID"> The meeting ID that identifies the meeting you are attempting to end </param>
        /// <param name="password"> The moderator password for this meeting (optional in BBB 3.x) </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<EndResponse>> EndAsync (string meetingID, string password = null);

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
        /// <param name="state"> The parameter state can be used to filter results. It can be a set of states separate by commas. If it is not specified only the states [published|unpublished] are considered (same as in previous versions). If it is specified as "any", recordings in all states are included </param>
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
        /// Retrieve the default config.xml. Deprecated in BBB 3.x.
        /// </summary>
        /// <returns> The config.xml as string</returns>
        [Obsolete("getDefaultConfigXML has been removed in BigBlueButton 3.x.")]
        Task<string> GetDefaultConfigXmlAsync();

        /// <summary>
        /// Associate a custom config.xml file with the current session. Deprecated in BBB 3.x.
        /// </summary>
        /// <param name="meetingID"> A meetingID to an active meeting </param>
        /// <param name="configXML"> A valid config.xml file </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        [Obsolete("setDefaultConfigXML has been removed in BigBlueButton 3.x.")]
        Task<string> SetDefaultConfigXmlAsync(string meetingID, string configXML);

        /// <summary>
        /// Get a list of the recordings text tracks for a recording
        /// </summary>
        /// <param name="recordID"> A record ID to get the text tracks for </param>
        /// <returns> The response content as string </returns>
        Task<string> GetRecordingTextTracksAsync(string recordID);

        /// <summary>
        /// Upload a caption or subtitle file to a recording
        /// </summary>
        /// <param name="recordID"> The record ID to upload the text track to </param>
        /// <param name="kind"> The kind of text track (e.g. "subtitles" or "captions") </param>
        /// <param name="lang"> The language of the text track (e.g. "en") </param>
        /// <param name="label"> The label for the text track (e.g. "English") </param>
        /// <returns> The response content as string </returns>
        Task<string> PutRecordingTextTrackAsync(string recordID, string kind, string lang, string label);

        // BBB 3.x API endpoints

        /// <summary>
        /// Insert one or more documents into a running meeting (BBB 3.x).
        /// The documents are provided in the request body in the same XML format as the create call.
        /// </summary>
        /// <param name="meetingID"> The meeting ID of the running meeting </param>
        /// <param name="requestBody"> A Request body containing data for the documents to insert </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<InsertDocumentResponse>> InsertDocumentAsync(string meetingID, CreateRequest requestBody);

        /// <summary>
        /// Send a chat message to a running meeting (BBB 3.x).
        /// </summary>
        /// <param name="meetingID"> The meeting ID of the running meeting </param>
        /// <param name="message"> The chat message to send </param>
        /// <param name="userName"> The display name for the message sender (defaults to "System") </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<SendChatMessageResponse>> SendChatMessageAsync(string meetingID, string message, string userName = null);
    }
}
