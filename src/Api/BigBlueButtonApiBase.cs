using System.Net;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using RestSharp;

namespace Avaco.BigBlueButton.Api
{
    /// <summary>
    /// This class provides some base base methods for a big blue button client adapter
    /// </summary>
    public class BigBlueButtonApiBase 
    {
        /// <summary>
        /// The RestSharp client used for the communication with the big blue button server 
        /// </summary>
        protected RestClient Client { get; private set; }

        /// <summary>
        /// The Secret used for authentication at the big blue button server
        /// </summary>
        protected string Secret { get; private set; }

        /// <summary>
        /// A constructor utilizing a host as well as a secret to setup a connetion to a big blue button server
        /// </summary>
        /// <param name="host"> The server host </param>
        /// <param name="secret"> The secret used for authentication</param>
        public BigBlueButtonApiBase (string host, string secret) {
            Initialize (host, false);
            Secret = secret;
        }

        /// <summary>
        /// A constructor utilizing a host as well as a secret to setup a connetion to a big blue button server
        /// </summary>
        /// <param name="host"> The server host </param>
        /// <param name="secret"> The secret used for authentication</param>
        /// <parma name="ignoreSslErrors"> An indicator if the connection shall ignore SSL errors like invalid certificates </param>
        public BigBlueButtonApiBase (string host, string secret, bool ignoreSslErrors) {
            Initialize (host, ignoreSslErrors);
            Secret = secret;
        }

        /// <summary>
        /// A initialization function for the RestSharp client
        /// </summary>
        /// <param name="host"> The server host </param>
        /// <param name="ignoreSslErrors"> An indicator if the connection shall ignore SSL errors like invalid certificates </param>
        /// <returns></returns>
        public bool Initialize (string host, bool ignoreSslErrors) {
            Client = new RestClient (host);
            if(ignoreSslErrors){
                Client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
            return true;
        }

        /// <summary>
        /// This function generates the checksum for the big blue button request, this is used as a authorization 
        /// method for each call to the big blue button server.
        /// For more inforation please have a look into http://docs.bigbluebutton.org/dev/api.html
        /// </summary>
        /// <param name="name"> The name of the api call </param>
        /// <param name="query"> The query string that is passed to the api call</param>
        /// <returns>The generated checksum</returns>
        public string CreateChecksum (string name, string query) {
            var sha = new SHA1Managed ();
            sha.ComputeHash (Encoding.UTF8.GetBytes ($"{name}{query}{Secret}"));
            return string.Concat (sha.Hash.Select (b => b.ToString ("x2")));
        }

        /// <summary>
        /// This function generates the checksum for the big blue button request, this is used as a authorization 
        /// method for each call to the big blue button server.
        /// For more inforation please have a look into http://docs.bigbluebutton.org/dev/api.html
        /// </summary>
        /// <param name="name"> The name of the api call </param>
        /// <param name="parameters"> A list of Parameter objects taken from the RestSharp client</param>
        /// <returns>The generated checksum</returns>
        public string CreateChecksum (string name, List<Parameter> parameters) {
            var query=string.Join("&",parameters.Where(p => p.Type == ParameterType.QueryStringWithoutEncode).Select(p => p.Name+"="+p.Value.ToString()));
            var checksum = CreateChecksum(name, query);
            return checksum;
        }

        /// <summary>
        /// This function adds a new query parameter of type string to a request
        /// </summary>
        /// <param name="request"> The request the new parameter is added to</param>
        /// <param name="name"> The name of the query parameter </param>
        /// <param name="parameter"> The value of the query parameter</param>
        public void AddQueryParameter(IRestRequest request, string name, string parameter)
        {          
            if (parameter != null) request.AddParameter(name, EncodeQueryParameter(parameter),ParameterType.QueryStringWithoutEncode);
        }

        /// <summary>
        /// This function adds a new query parameter of type bool to a request
        /// </summary>
        /// <param name="request"> The request the new parameter is added to</param>
        /// <param name="name"> The name of the query parameter </param>
        /// <param name="parameter"> The value of the query parameter</param>
        public void AddQueryParameter(IRestRequest request, string name, bool? parameter)
        {          
            if (parameter != null) request.AddParameter(name, EncodeQueryParameter(parameter),ParameterType.QueryStringWithoutEncode);
        }

        /// <summary>
        /// This function adds a new query parameter of type long to a request
        /// </summary>
        /// <param name="request"> The request the new parameter is added to</param>
        /// <param name="name"> The name of the query parameter </param>
        /// <param name="parameter"> The value of the query parameter</param>
        public void AddQueryParameter(IRestRequest request, string name, long? parameter)
        {          
            if (parameter != null) request.AddParameter(name, EncodeQueryParameter(parameter),ParameterType.QueryStringWithoutEncode);
        }

        /// <summary>
        /// This function calculates the request checksum and adds it to the query parameters of the request
        /// </summary>
        /// <param name="request"> The request thats checksum needs to be calculated and added </param>
        /// <param name="operation"> The name of the operation </param>
        public void AddQueryChecksum(IRestRequest request, string operation) {
            var checksum = CreateChecksum(operation, request.Parameters);          
            request.AddQueryParameter ("checksum", checksum);
        }

        /// <summary>
        /// This funciton escapes a string query parameter in a way that big blue button is able to process it
        /// </summary>
        /// <param name="value">The value that needs to be escaped </param>
        /// <returns> The escaped value </returns>
        public static string EncodeQueryParameter (string value) {
            return WebUtility.UrlEncode (value.ToString())
                .Replace ("\\%28", "(")
                .Replace ("\\%29", ")")
                .Replace ("\\+", "%20")
                .Replace ("\\%27", "'")
                .Replace ("\\%21", "!")
                .Replace ("\\%7E", "~");
        }

        /// <summary>
        /// This funciton escapes a bool query parameter in a way that big blue button is able to process it
        /// </summary>
        /// <param name="value">The value that needs to be escaped </param>
        /// <returns> The escaped value </returns>
        public static string EncodeQueryParameter (bool? value) {
            return WebUtility.UrlEncode (Convert.ToString(value).ToLower());
        }

        /// <summary>
        /// This funciton escapes a long query parameter in a way that big blue button is able to process it
        /// </summary>
        /// <param name="value">The value that needs to be escaped </param>
        /// <returns> The escaped value </returns>
        public static string EncodeQueryParameter (long? value) {
            return WebUtility.UrlEncode (Convert.ToString(value).ToLower());
        }
    }
}