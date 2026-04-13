using System;
using System.Threading.Tasks;
using Avaco.BigBlueButton.Api.Models.Request;
using Avaco.BigBlueButton.Api.Models.Response;
using Avaco.BigBlueButton.Rest;

namespace Avaco.BigBlueButton.Api.Interfaces {
    /// <summary>
    /// This interface defines all methods for communicating with a BigBlueButton 3.x server.
    /// Extends the base IBigBlueButtonApi interface with BBB 3.x-specific features.
    /// https://docs.bigbluebutton.org/development/api/
    /// </summary>
    public interface IBigBlueButtonApiV3 : IBigBlueButtonApi {

        /// <summary>
        /// Creates a new meeting room with all available parameters including BBB 3.x additions.
        /// Uses SHA256 checksum by default. Supports role-based joining (no passwords required).
        /// </summary>
        /// <param name="meetingID"> The meeting ID used to reference the meeting in subsequent calls </param>
        /// <param name="name"> The name of the meeting room </param>
        /// <param name="attendeePW"> The password for attendees (optional in BBB 3.x, use role-based join instead) </param>
        /// <param name="moderatorPW"> The password for moderators (optional in BBB 3.x, use role-based join instead) </param>
        /// <param name="welcome"> A welcome message to be shown on the meeting room </param>
        /// <param name="dialNumber"> A dial number users can call to join by phone </param>
        /// <param name="voiceBridge"> A PIN number for phone users </param>
        /// <param name="maxParticipants"> The maximum amount of participants for the meeting </param>
        /// <param name="logoutURL"> A url used for redirection after logout </param>
        /// <param name="record"> An indicator if the meeting should be recorded </param>
        /// <param name="duration"> The maximum length (in minutes) for the meeting </param>
        /// <param name="isBreakout"> Must be set to true to create a breakout room </param>
        /// <param name="parentMeetingID"> Must be provided when creating a breakout room </param>
        /// <param name="sequence"> The sequence number of the breakout room </param>
        /// <param name="freeJoin"> If set to true, the user can choose which breakout room to join </param>
        /// <param name="moderatorOnlyMessage"> Display a message to all moderators in the public chat </param>
        /// <param name="autoStartRecording"> Whether to automatically start recording when first user joins </param>
        /// <param name="allowStartStopRecording"> Allow the user to start/stop recording </param>
        /// <param name="webcamsOnlyForModerator"> Webcams shared by viewers only appear for moderators </param>
        /// <param name="logo"> URL of a custom logo to replace the default </param>
        /// <param name="bannerText"> Banner text in the client </param>
        /// <param name="bannerColor"> Banner background color (Hex format) </param>
        /// <param name="copyright"> Custom copyright on the footer </param>
        /// <param name="muteOnStart"> Mute all users when the meeting starts </param>
        /// <param name="allowModsToUnmuteUsers"> Allow moderators to unmute other users </param>
        /// <param name="lockSettingsDisableCam"> Prevent users from sharing their camera </param>
        /// <param name="lockSettingsDisableMic"> Only allow user to join listen only </param>
        /// <param name="lockSettingsDisablePrivateChat"> Disable private chats </param>
        /// <param name="lockSettingsDisablePublicChat"> Disable public chat </param>
        /// <param name="lockSettingsDisableNote"> Disable notes </param>
        /// <param name="lockSettingsLockedLayout"> Lock the layout </param>
        /// <param name="lockSettingsLockOnJoin"> Apply lock settings on join </param>
        /// <param name="lockSettingsLockOnJoinConfigurable"> Allow configuring lockSettingsLockOnJoin </param>
        /// <param name="guestPolicy"> Guest policy: ALWAYS_ACCEPT, ALWAYS_DENY, or ASK_MODERATOR </param>
        /// <param name="meta"> Metadata values (format: meta_name=value) </param>
        /// <param name="allowDuplicateExtUserid"> Allow joining from multiple devices </param>
        /// <param name="meetingExpireWhenLastUserLeftInMinutes"> Minutes to wait after last user leaves </param>
        /// <param name="meetingLayout"> The default meeting layout </param>
        /// <param name="endWhenNoModerator"> End meeting when no moderator is present </param>
        /// <param name="endWhenNoModeratorDelayInMinutes"> Delay before ending when no moderator </param>
        /// <param name="meetingKeepEvents"> Save meeting events even if not recorded </param>
        /// <param name="allowModsToEjectCameras"> Allow moderators to eject webcams </param>
        /// <param name="meetingCameraCap"> Maximum webcams for the meeting (0 = unlimited) </param>
        /// <param name="userCameraCap"> Maximum webcams per user </param>
        /// <param name="meetingExpireIfNoUserJoinedInMinutes"> Minutes before expiring if no user joins </param>
        /// <param name="meetingEndedURL"> Callback URL when meeting ends </param>
        /// <param name="disabledFeatures"> Comma-separated list of features to disable </param>
        /// <param name="disabledFeaturesExclude"> Comma-separated list of features to re-enable </param>
        /// <param name="notifyRecordingIsOn"> Notify users when recording is on </param>
        /// <param name="presentationUploadExternalUrl"> External URL for presentation upload </param>
        /// <param name="presentationUploadExternalDescription"> Description for external presentation upload </param>
        /// <param name="learningDashboardCleanupDelayInMinutes"> Delay before cleaning up learning dashboard data </param>
        /// <param name="recordFullDurationMedia"> Record full duration if meeting is recorded </param>
        /// <param name="lockSettingsHideUserList"> Hide user list from viewers </param>
        /// <param name="lockSettingsHideViewersCursor"> Hide viewers cursor from other viewers </param>
        /// <param name="lockSettingsHideViewersAnnotation"> Hide viewers annotation from other viewers </param>
        /// <param name="groups"> JSON string defining user groups </param>
        /// <param name="breakoutRoomsRecord"> Whether breakout rooms should be recorded </param>
        /// <param name="breakoutRoomsPrivateChatEnabled"> Whether private chat is enabled in breakout rooms </param>
        /// <param name="allowPromoteGuestToModerator"> Allow promoting guest users to moderators </param>
        /// <param name="preUploadedPresentationOverrideDefault"> Override default presentation with pre-uploaded one </param>
        /// <param name="requestBody"> A Request body containing data for preuploaded slides </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<CreateResponse>> CreateAsyncV3 (
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
        /// Join a meeting using role-based authentication (BBB 3.x).
        /// The 'role' parameter is the preferred way to assign user roles in BBB 3.x.
        /// The 'password' parameter is still supported for backward compatibility.
        /// </summary>
        /// <param name="fullName"> The full name to identify this user </param>
        /// <param name="meetingID"> The meeting ID to join </param>
        /// <param name="password"> The password (deprecated in BBB 3.x, use role instead) </param>
        /// <param name="role"> The role: "MODERATOR" or "VIEWER" </param>
        /// <param name="createTime"> Must match the 'createTime' for the session </param>
        /// <param name="userID"> An identifier for this user </param>
        /// <param name="webVoiceConfig"> Custom voice-extension for VOIP </param>
        /// <param name="defaultLayout"> The layout name to be loaded first </param>
        /// <param name="avatarURL"> The URL for the user's avatar </param>
        /// <param name="redirect"> Controls whether the browser is redirected to the client </param>
        /// <param name="clientURL"> Custom client URL </param>
        /// <param name="guest"> Set to "true" to indicate a guest user </param>
        /// <param name="enforceLayout"> The layout to enforce for this user </param>
        /// <param name="excludeFromDashboard"> Exclude this user from the learning dashboard </param>
        /// <param name="errorRedirectUrl"> URL to redirect to in case of an error </param>
        /// <param name="userdata"> Parameters for customization (format: userdata-name=value) </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<JoinResponse>> JoinAsyncV3 (
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
        /// Generate a join URI using role-based authentication (BBB 3.x) without executing the request.
        /// </summary>
        /// <param name="fullName"> The full name to identify this user </param>
        /// <param name="meetingID"> The meeting ID to join </param>
        /// <param name="password"> The password (deprecated in BBB 3.x) </param>
        /// <param name="role"> The role: "MODERATOR" or "VIEWER" </param>
        /// <param name="createTime"> Must match the 'createTime' for the session </param>
        /// <param name="userID"> An identifier for this user </param>
        /// <param name="webVoiceConfig"> Custom voice-extension for VOIP </param>
        /// <param name="defaultLayout"> The layout name to be loaded first </param>
        /// <param name="avatarURL"> The URL for the user's avatar </param>
        /// <param name="redirect"> Controls whether the browser is redirected </param>
        /// <param name="clientURL"> Custom client URL </param>
        /// <param name="guest"> Set to "true" for guest users </param>
        /// <param name="enforceLayout"> The layout to enforce </param>
        /// <param name="excludeFromDashboard"> Exclude from learning dashboard </param>
        /// <param name="errorRedirectUrl"> Error redirect URL </param>
        /// <param name="userdata"> Parameters for customization </param>
        /// <returns> The URI to join the meeting </returns>
        Uri JoinUriV3 (
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
        /// End a meeting. In BBB 3.x, authentication is done via the checksum (shared secret),
        /// so the password parameter is not required.
        /// </summary>
        /// <param name="meetingID"> The meeting ID to end </param>
        /// <param name="password"> The moderator password (optional, for backward compatibility) </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<EndResponse>> EndAsyncV3 (string meetingID, string password = null);

        /// <summary>
        /// Insert one or more documents into a running meeting.
        /// The documents are provided in the request body in the same XML format as the create call.
        /// </summary>
        /// <param name="meetingID"> The meeting ID of the running meeting </param>
        /// <param name="requestBody"> A Request body containing data for the documents to insert </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<InsertDocumentResponse>> InsertDocumentAsync(string meetingID, CreateRequest requestBody);

        /// <summary>
        /// Send a chat message to a running meeting.
        /// </summary>
        /// <param name="meetingID"> The meeting ID of the running meeting </param>
        /// <param name="message"> The chat message to send </param>
        /// <param name="userName"> The display name for the message sender (defaults to "System") </param>
        /// <returns> A response object containing the HTTP status code as well as the returned data </returns>
        Task<RestApiResponse<SendChatMessageResponse>> SendChatMessageAsync(string meetingID, string message, string userName = null);
    }
}
