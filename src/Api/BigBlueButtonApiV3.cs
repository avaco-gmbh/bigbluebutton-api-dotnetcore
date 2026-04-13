using RestSharp;

using Avaco.BigBlueButton.Api.Interfaces;
using Avaco.BigBlueButton.Api.Models.Request;
using Avaco.BigBlueButton.Api.Models.Response;
using Avaco.BigBlueButton.Rest;
using RestSharp.Serializers;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Avaco.BigBlueButton.Api
{

    /// <summary>
    /// This class implements a client adapter for BigBlueButton servers version 3.x.
    /// It extends the V2.2 adapter and adds support for BBB 3.x-specific features:
    /// - SHA256 checksum by default (configurable to SHA384/SHA512)
    /// - Role-based join (no passwords required)
    /// - New create parameters (disabledFeatures, endWhenNoModerator, camera caps, etc.)
    /// - New join parameters (role, enforceLayout, excludeFromDashboard, errorRedirectUrl)
    /// - New endpoints (insertDocument, sendChatMessage)
    /// - Password is optional for the end API
    /// </summary>
    /// <inheritdoc/>
    public class BigBlueButtonApiV3 : BigBlueButtonApiV2_2, IBigBlueButtonApiV3
    {

        /// <summary>
        /// Creates a new BBB 3.x client adapter using SHA256 checksum by default
        /// </summary>
        /// <param name="host">The server host</param>
        /// <param name="secret">The secret used for authentication</param>
        /// <param name="ignoreSslErrors">An indicator if the connection shall ignore SSL errors</param>
        public BigBlueButtonApiV3(string host, string secret, bool ignoreSslErrors) : base(host, secret, ignoreSslErrors, ChecksumHashAlgorithm.SHA256)
        {
        }

        /// <summary>
        /// Creates a new BBB 3.x client adapter using SHA256 checksum by default
        /// </summary>
        /// <param name="host">The server host</param>
        /// <param name="secret">The secret used for authentication</param>
        public BigBlueButtonApiV3(string host, string secret) : base(host, secret, false, ChecksumHashAlgorithm.SHA256)
        {
        }

        /// <summary>
        /// Creates a new BBB 3.x client adapter with a specific checksum hash algorithm
        /// </summary>
        /// <param name="host">The server host</param>
        /// <param name="secret">The secret used for authentication</param>
        /// <param name="ignoreSslErrors">An indicator if the connection shall ignore SSL errors</param>
        /// <param name="hashAlgorithm">The hash algorithm to use for checksum generation</param>
        public BigBlueButtonApiV3(string host, string secret, bool ignoreSslErrors, ChecksumHashAlgorithm hashAlgorithm) : base(host, secret, ignoreSslErrors, hashAlgorithm)
        {
        }

        /// <summary>
        /// Creates a new meeting room with all BBB 3.x parameters.
        /// Includes new parameters such as disabledFeatures, endWhenNoModerator, camera caps,
        /// learning dashboard settings, and more.
        /// </summary>
        public async Task<RestApiResponse<CreateResponse>> CreateAsyncV3(
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
        )
        {
            IRestRequest req = new RestRequest("create", Method.POST, DataFormat.Xml);
            AddQueryParameter(req, "name", name);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "attendeePW", attendeePW);
            AddQueryParameter(req, "moderatorPW", moderatorPW);
            AddQueryParameter(req, "welcome", welcome);
            AddQueryParameter(req, "dialNumber", dialNumber);
            AddQueryParameter(req, "voiceBridge", voiceBridge);
            AddQueryParameter(req, "maxParticipants", maxParticipants);
            AddQueryParameter(req, "logoutURL", logoutURL);
            AddQueryParameter(req, "record", record);
            AddQueryParameter(req, "duration", duration);
            if (isBreakout.HasValue)
            {
                AddQueryParameter(req, "isBreakout", isBreakout);
                AddQueryParameter(req, "parentMeetingID", parentMeetingID);
                AddQueryParameter(req, "sequence", sequence);
                AddQueryParameter(req, "freeJoin", freeJoin);
            }
            AddQueryParameter(req, "moderatorOnlyMessage", moderatorOnlyMessage);
            AddQueryParameter(req, "autoStartRecording", autoStartRecording);
            AddQueryParameter(req, "allowStartStopRecording", allowStartStopRecording);
            AddQueryParameter(req, "webcamsOnlyForModerator", webcamsOnlyForModerator);
            AddQueryParameter(req, "logo", logo);
            AddQueryParameter(req, "bannerText", bannerText);
            AddQueryParameter(req, "bannerColor", bannerColor);
            AddQueryParameter(req, "copyright", copyright);
            AddQueryParameter(req, "muteOnStart", muteOnStart);
            AddQueryParameter(req, "allowModsToUnmuteUsers", allowModsToUnmuteUsers);
            AddQueryParameter(req, "lockSettingsDisableCam", lockSettingsDisableCam);
            AddQueryParameter(req, "lockSettingsDisableMic", lockSettingsDisableMic);
            AddQueryParameter(req, "lockSettingsDisablePrivateChat", lockSettingsDisablePrivateChat);
            AddQueryParameter(req, "lockSettingsDisablePublicChat", lockSettingsDisablePublicChat);
            AddQueryParameter(req, "lockSettingsDisableNote", lockSettingsDisableNote);
            AddQueryParameter(req, "lockSettingsLockedLayout", lockSettingsLockedLayout);
            AddQueryParameter(req, "lockSettingsLockOnJoin", lockSettingsLockOnJoin);
            AddQueryParameter(req, "lockSettingsLockOnJoinConfigurable", lockSettingsLockOnJoinConfigurable);
            AddQueryParameter(req, "guestPolicy", guestPolicy);
            AddQueryParameter(req, "allowDuplicateExtUserid", allowDuplicateExtUserid);
            AddQueryParameter(req, "meetingExpireWhenLastUserLeftInMinutes", meetingExpireWhenLastUserLeftInMinutes);
            AddQueryParameter(req, "meetingLayout", meetingLayout);
            // BBB 2.5+ / 3.x parameters
            AddQueryParameter(req, "endWhenNoModerator", endWhenNoModerator);
            AddQueryParameter(req, "endWhenNoModeratorDelayInMinutes", endWhenNoModeratorDelayInMinutes);
            AddQueryParameter(req, "meetingKeepEvents", meetingKeepEvents);
            AddQueryParameter(req, "allowModsToEjectCameras", allowModsToEjectCameras);
            AddQueryParameter(req, "meetingCameraCap", meetingCameraCap);
            AddQueryParameter(req, "userCameraCap", userCameraCap);
            AddQueryParameter(req, "meetingExpireIfNoUserJoinedInMinutes", meetingExpireIfNoUserJoinedInMinutes);
            AddQueryParameter(req, "meetingEndedURL", meetingEndedURL);
            AddQueryParameter(req, "disabledFeatures", disabledFeatures);
            AddQueryParameter(req, "disabledFeaturesExclude", disabledFeaturesExclude);
            AddQueryParameter(req, "notifyRecordingIsOn", notifyRecordingIsOn);
            AddQueryParameter(req, "presentationUploadExternalUrl", presentationUploadExternalUrl);
            AddQueryParameter(req, "presentationUploadExternalDescription", presentationUploadExternalDescription);
            AddQueryParameter(req, "learningDashboardCleanupDelayInMinutes", learningDashboardCleanupDelayInMinutes);
            AddQueryParameter(req, "recordFullDurationMedia", recordFullDurationMedia);
            AddQueryParameter(req, "lockSettingsHideUserList", lockSettingsHideUserList);
            AddQueryParameter(req, "lockSettingsHideViewersCursor", lockSettingsHideViewersCursor);
            AddQueryParameter(req, "lockSettingsHideViewersAnnotation", lockSettingsHideViewersAnnotation);
            AddQueryParameter(req, "groups", groups);
            AddQueryParameter(req, "breakoutRoomsRecord", breakoutRoomsRecord);
            AddQueryParameter(req, "breakoutRoomsPrivateChatEnabled", breakoutRoomsPrivateChatEnabled);
            AddQueryParameter(req, "allowPromoteGuestToModerator", allowPromoteGuestToModerator);
            AddQueryParameter(req, "preUploadedPresentationOverrideDefault", preUploadedPresentationOverrideDefault);
            if (meta != null)
            {
                foreach (var m in meta)
                {
                    var kv = m.Split('=');
                    if (kv.Length < 2 || !kv[0].StartsWith("meta_")) throw new ArgumentException("the meta parameters need to be of format meta_<name>=<value>");
                    AddQueryParameter(req, kv[0], kv[1]);
                }
            }
            AddQueryChecksum(req, "create");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            if (requestBody != null)
            {
                req.XmlSerializer = new DotNetXmlSerializer();
                req.AddXmlBody(requestBody);
            }

            var response = await Client.ExecuteAsync<CreateResponse>(req);
            return new RestApiResponse<CreateResponse>(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Builds the join request with BBB 3.x parameters including role-based authentication
        /// </summary>
        private IRestRequest JoinBuildRequestV3(
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
        )
        {
            IRestRequest req = new RestRequest("join", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "fullName", fullName);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "password", password);
            AddQueryParameter(req, "role", role);
            AddQueryParameter(req, "createTime", createTime);
            AddQueryParameter(req, "userID", userID);
            AddQueryParameter(req, "webVoiceConfig", webVoiceConfig);
            AddQueryParameter(req, "defaultLayout", defaultLayout);
            AddQueryParameter(req, "avatarURL", avatarURL);
            AddQueryParameter(req, "redirect", redirect);
            AddQueryParameter(req, "clientURL", clientURL);
            AddQueryParameter(req, "guest", guest);
            AddQueryParameter(req, "enforceLayout", enforceLayout);
            AddQueryParameter(req, "excludeFromDashboard", excludeFromDashboard);
            AddQueryParameter(req, "errorRedirectUrl", errorRedirectUrl);
            if (userdata != null)
            {
                foreach (var m in userdata)
                {
                    var kv = m.Split('=');
                    if (kv.Length < 2 || !kv[0].StartsWith("userdata-")) throw new ArgumentException("the userdata parameters need to be of format userdata-<name>=<value>");
                    AddQueryParameter(req, kv[0], kv[1]);
                }
            }
            AddQueryChecksum(req, "join");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            return req;
        }

        /// <summary>
        /// Join a meeting using role-based authentication (BBB 3.x).
        /// The 'role' parameter ("MODERATOR" or "VIEWER") is the preferred way to assign roles.
        /// The 'password' parameter is still supported for backward compatibility.
        /// </summary>
        public async Task<RestApiResponse<JoinResponse>> JoinAsyncV3(
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
        )
        {
            IRestRequest req = JoinBuildRequestV3(fullName, meetingID, password, role, createTime, userID, webVoiceConfig, defaultLayout, avatarURL, redirect, clientURL, guest, enforceLayout, excludeFromDashboard, errorRedirectUrl, userdata);
            var response = await Client.ExecuteAsync<JoinResponse>(req);
            return new RestApiResponse<JoinResponse>(response.StatusCode, response.Data, response.Cookies?.ToDictionary(c => c.Name, c => c.Value));
        }

        /// <summary>
        /// Generate a join URI using role-based authentication (BBB 3.x) without executing the request.
        /// </summary>
        public Uri JoinUriV3(
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
        )
        {
            IRestRequest req = JoinBuildRequestV3(fullName, meetingID, password, role, createTime, userID, webVoiceConfig, defaultLayout, avatarURL, redirect, clientURL, guest, enforceLayout, excludeFromDashboard, errorRedirectUrl, userdata);
            return Client.BuildUri(req);
        }

        /// <summary>
        /// End a meeting. In BBB 3.x, authentication is done via the checksum (shared secret),
        /// so the password parameter is optional.
        /// </summary>
        public async Task<RestApiResponse<EndResponse>> EndAsyncV3(string meetingID, string password = null)
        {
            IRestRequest req = new RestRequest("end", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "password", password);
            AddQueryChecksum(req, "end");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<EndResponse>(req);
            return new RestApiResponse<EndResponse>(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Insert one or more documents into a running meeting.
        /// The documents are provided in the request body in the same XML format as the create call.
        /// </summary>
        public async Task<RestApiResponse<InsertDocumentResponse>> InsertDocumentAsync(string meetingID, CreateRequest requestBody)
        {
            IRestRequest req = new RestRequest("insertDocument", Method.POST, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryChecksum(req, "insertDocument");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            if (requestBody != null)
            {
                req.XmlSerializer = new DotNetXmlSerializer();
                req.AddXmlBody(requestBody);
            }
            var response = await Client.ExecuteAsync<InsertDocumentResponse>(req);
            return new RestApiResponse<InsertDocumentResponse>(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Send a chat message to a running meeting.
        /// </summary>
        public async Task<RestApiResponse<SendChatMessageResponse>> SendChatMessageAsync(string meetingID, string message, string userName = null)
        {
            IRestRequest req = new RestRequest("sendChatMessage", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "message", message);
            AddQueryParameter(req, "userName", userName);
            AddQueryChecksum(req, "sendChatMessage");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<SendChatMessageResponse>(req);
            return new RestApiResponse<SendChatMessageResponse>(response.StatusCode, response.Data);
        }
    }
}
