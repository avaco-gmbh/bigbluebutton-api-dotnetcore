
using RestSharp;

using Avaco.BigBlueButton.Api.Interfaces;
using Avaco.BigBlueButton.Api.Models.Request;
using Avaco.BigBlueButton.Api.Models.Response;
using Avaco.BigBlueButton.Rest;
using RestSharp.Serializers;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Avaco.BigBlueButton.Api {

    /// <summary>
    /// This class implements a client adapter for big blue button servers using version 2.2 and upwards
    /// </summary>
    /// <inheritdoc/>   
    public class BigBlueButtonApiV2_2 : BigBlueButtonApiBase, IBigBlueButtonApi {


        public BigBlueButtonApiV2_2 (string host, string secret, bool ignoreSslErrors) : base(host, secret, ignoreSslErrors ){
        }
        public BigBlueButtonApiV2_2 (string host, string secret) : base(host, secret, false){
        }

        public BigBlueButtonApiV2_2 (string secret) : base ("http://localhost", secret, false) {

        }
        
        public async Task<RestApiResponse<CreateResponse>> CreateAsync(string meetingID,string name = null,string attendeePW = null,string moderatorPW = null,string welcome = null, CreateRequest requestBody = null)
            => await CreateAsync(meetingID,name , attendeePW , moderatorPW, welcome, requestBody);
        public async Task<RestApiResponse<CreateResponse>> CreateAsync (
            string meetingID, 
            string name = null, 
            string attendeePW = null, 
            string moderatorPW = null, 
            string welcome = null, 
            string dialNumber = null, 
            string voiceBridge=null, 
            long? maxParticipants=null,
            string logoutURL=null,
            bool record = false,
            long? duration = null,
            bool? isBreakout = null,
            string parentMeetingID = null,
            long? sequence = null,
            bool? freeJoin = null,
            string moderatorOnlyMessage = null,
            bool autoStartRecording = false,
            bool allowStartStopRecording = false,
            bool? webcamsOnlyForModerator= null,
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
        ) {
            IRestRequest req = new RestRequest ("create", Method.POST, DataFormat.Xml);
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
            if (isBreakout.HasValue){
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
            AddQueryParameter(req, "lockSettingsLockedLayout", lockSettingsLockedLayout);
            AddQueryParameter(req, "lockSettingsLockOnJoin", lockSettingsLockOnJoin);
            AddQueryParameter(req, "lockSettingsLockOnJoinConfigurable", lockSettingsLockOnJoinConfigurable);
            AddQueryParameter(req, "guestPolicy", guestPolicy);
            AddQueryParameter(req, "allowDuplicateExtUserid", allowDuplicateExtUserid);
            if (meta != null ){
                foreach(var m in meta){
                    var kv = m.Split('=');
                    if(kv.Length<2 || !kv[0].StartsWith("meta_"))throw new ArgumentException("the meta parameters need to be of format meta_<name>=<value>");
                    AddQueryParameter(req, kv[0], kv[1]);
                }
            }
            AddQueryChecksum(req, "create");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");       
            if(requestBody != null){
                req.XmlSerializer = new DotNetXmlSerializer();
                req.AddXmlBody(requestBody);
            } 
                
            var response = await Client.ExecuteAsync<CreateResponse>(req); 
            return new RestApiResponse<CreateResponse>(response.StatusCode, response.Data);
        }

        /// <summary>
        /// A function to create the join requests 
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
        /// <param name="customStyle"> A string containing custom css data to customize the html5 client </param>
        /// <param name="customStyleUrl"> A url pointing to a custom css data to customize the html5 client</param>
        /// <returns>The request object used to do the request to the server</returns>
        private IRestRequest JoinBuildRequest(
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
            string guest = "false",
            string[] userdata = null
        )
        {
            IRestRequest req = new RestRequest ("join", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "fullName", fullName);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "password", password);
            AddQueryParameter(req, "createTime", createTime);
            AddQueryParameter(req, "userID", userID);
            AddQueryParameter(req, "webVoiceConfig", webVoiceConfig);
            AddQueryParameter(req, "configToken", configToken);
            AddQueryParameter(req, "defaultLayout", defaultLayout);
            AddQueryParameter(req, "avatarURL", avatarURL);
            AddQueryParameter(req, "redirect", redirect);
            AddQueryParameter(req, "clientURL", clientURL);
            AddQueryParameter(req, "joinViaHtml5", joinViaHtml5);
            AddQueryParameter(req, "guest", guest);
            if (userdata != null)
            {
                foreach (var m in userdata)
                {
                    var kv = m.Split('=');
                    if (kv.Length < 2 || !kv[0].StartsWith("userdata-")) throw new ArgumentException("the meta parameters need to be of format userdata-<name>=<value>");
                    AddQueryParameter(req, kv[0], kv[1]);
                }
            }
            AddQueryChecksum(req, "join");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            return req;
        }

        public async Task<RestApiResponse<JoinResponse>> JoinAsync (
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
        ) {
            IRestRequest req = JoinBuildRequest(fullName,meetingID,password,createTime,userID,webVoiceConfig,configToken,defaultLayout,avatarURL,redirect,clientURL,joinViaHtml5,guest,userdata);
            var response = await Client.ExecuteAsync<JoinResponse>(req); 
            return new RestApiResponse<JoinResponse>(response.StatusCode, response.Data, response.Cookies?.ToDictionary(c => c.Name, c => c.Value));
        }


        public Uri JoinUri(
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
        )
        {
            IRestRequest req = JoinBuildRequest(fullName,meetingID,password,createTime,userID,webVoiceConfig,configToken,defaultLayout,avatarURL,redirect,clientURL,joinViaHtml5,guest,userdata);
            return Client.BuildUri(req);
        }

        public async Task<RestApiResponse<IsMeetingRunningResponse>> IsMeetingRunningAsync (string meetingID) {
            IRestRequest req = new RestRequest ("isMeetingRunning", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryChecksum(req, "isMeetingRunning");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response =  await Client.ExecuteAsync<IsMeetingRunningResponse>(req); 
            return new RestApiResponse<IsMeetingRunningResponse>(response.StatusCode, response.Data);
        }

        public async Task<RestApiResponse<EndResponse>> EndAsync(string meetingID, string password){
            IRestRequest req = new RestRequest ("end", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "password", password);
            AddQueryChecksum(req, "end");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<EndResponse>(req); 
            return new RestApiResponse<EndResponse>(response.StatusCode, response.Data);
        }

        public async Task<RestApiResponse<GetMeetingInfoResponse>> GetMeetingInfoAsync (string meetingID) {
            IRestRequest req = new RestRequest ("getMeetingInfo", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryChecksum(req, "getMeetingInfo");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<GetMeetingInfoResponse>(req); 
            return new RestApiResponse<GetMeetingInfoResponse>(response.StatusCode, response.Data);
        }

        public async Task<RestApiResponse<GetMeetingsResponse>> GetMeetingsAsync (){
            IRestRequest req = new RestRequest ("getMeetings", Method.GET, DataFormat.Xml);
            AddQueryChecksum(req, "getMeetings");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<GetMeetingsResponse>(req); 
            return new RestApiResponse<GetMeetingsResponse>(response.StatusCode, response.Data);
        }


        public async Task<RestApiResponse<GetRecordingsResponse>> GetRecordingsAsync (string meetingID = null, string recordID = null, string state = null, string[] meta = null) {
            IRestRequest req = new RestRequest ("getRecordings", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", meetingID);
            AddQueryParameter(req, "recordID", recordID);
            AddQueryParameter(req, "state", state);
            if(meta != null ){
                foreach(var m in meta){
                    var kv = m.Split('=');
                    if(kv.Length<2 || !kv[0].StartsWith("meta_"))throw new ArgumentException("the meta parameters need to be of format meta_<name>=<value>");
                    AddQueryParameter(req, kv[0], kv[1]);
                }
            }
            AddQueryChecksum(req, "getRecordings");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<GetRecordingsResponse>(req); 
            return new RestApiResponse<GetRecordingsResponse>(response.StatusCode, response.Data);
        }

        public async Task<RestApiResponse<PublishRecordingsResponse>> PublishRecordingsAsync (string recordID, bool publish) {
            IRestRequest req = new RestRequest ("publishRecordings", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "recordID", recordID);
            AddQueryParameter(req, "publish", publish);
            AddQueryChecksum(req, "publishRecordings");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<PublishRecordingsResponse>(req); 
            return new RestApiResponse<PublishRecordingsResponse>(response.StatusCode, response.Data);
        }

        public async Task<RestApiResponse<DeleteRecordingsResponse>> DeleteRecordingAsync (string recordID) {
            IRestRequest req = new RestRequest ("deleteRecordings", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "recordID", recordID);
            AddQueryChecksum(req, "deleteRecordings");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<DeleteRecordingsResponse>(req); 
            return new RestApiResponse<DeleteRecordingsResponse>(response.StatusCode, response.Data);
        }
        
        public async Task<RestApiResponse<UpdateRecordingsResponse>> UpdateRecordingsAsync (string recordID, string[] meta = null) {
            IRestRequest req = new RestRequest ("updateRecordings", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "recordID", recordID);
            if(meta != null ){
                foreach(var m in meta){
                    var kv = m.Split('=');
                    if(kv.Length<2 || !kv[0].StartsWith("meta_"))throw new ArgumentException("the meta parameters need to be of format meta_<name>=<value>");
                    AddQueryParameter(req, kv[0], kv[1]);
                }
            }
            AddQueryChecksum(req, "updateRecordings");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<UpdateRecordingsResponse>(req); 
            return new RestApiResponse<UpdateRecordingsResponse>(response.StatusCode, response.Data);
        }

        public async Task<string> GetDefaultConfigXmlAsync() {
            IRestRequest req = new RestRequest ("getDefaultConfigXML", Method.GET, DataFormat.Xml);
            AddQueryChecksum(req, "getDefaultConfigXML");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync(req); 
            return response.Content;
        }

        public async Task<string> SetDefaultConfigXmlAsync(string meetingID, string configXML) {
            IRestRequest req = new RestRequest ("getDefaultConfigXML", Method.POST, DataFormat.Xml);
            AddQueryParameter(req, "meetingID", configXML);
            AddQueryChecksum(req, "setDefaultConfigXML");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddXmlBody(configXML);
            var response = await Client.ExecuteAsync(req); 
            return response.Content;
        }

        public async Task<string> GetRecordingTextTracksAsync(string recordID) {
            IRestRequest req = new RestRequest ("getRecordingTextTracks", Method.GET, DataFormat.Xml);
            AddQueryParameter(req, "recordID", recordID);
            AddQueryChecksum(req, "getRecordingTextTracks");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            var response = await Client.ExecuteAsync<UpdateRecordingsResponse>(req); 
            return response.Content; 
        }

        public async Task<string> PutRecordingTextTrackAsync(string recordID,string kind, string lang, string label) {
            IRestRequest req = new RestRequest ("putRecordingTextTrack", Method.POST, DataFormat.None);
            AddQueryParameter(req, "recordID", recordID);
            AddQueryParameter(req, "kind", kind);
            AddQueryParameter(req, "lang", lang);
            AddQueryParameter(req, "label", label);
            AddQueryChecksum(req, "putRecordingTextTrack");
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            //req.AddFileBytes()
            //req.AlwaysMultipartFormData = true;
            var response = await Client.ExecuteAsync<UpdateRecordingsResponse>(req); 
            return response.Content; 
        }
    }
}