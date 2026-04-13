using System.Net;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using RestSharp;
using System.Web;

namespace Avaco.BigBlueButton.Api
{
    /// <summary>
    /// Defines the supported checksum hash algorithms for BigBlueButton API authentication.
    /// BigBlueButton 3.x supports SHA1, SHA256, SHA384, and SHA512.
    /// </summary>
    public enum ChecksumHashAlgorithm
    {
        /// <summary>SHA-1 (40-character hex output). Supported by BBB 2.x and 3.x.</summary>
        SHA1,
        /// <summary>SHA-256 (64-character hex output). Supported by BBB 2.6+ and 3.x.</summary>
        SHA256,
        /// <summary>SHA-384 (96-character hex output). Supported by BBB 2.6+ and 3.x.</summary>
        SHA384,
        /// <summary>SHA-512 (128-character hex output). Supported by BBB 2.6+ and 3.x.</summary>
        SHA512
    }

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
        /// The hash algorithm used for checksum generation. Defaults to SHA256 for BBB 3.x compatibility.
        /// </summary>
        protected ChecksumHashAlgorithm HashAlgorithm { get; private set; }

        /// <summary>
        /// A constructor utilizing a host as well as a secret to setup a connetion to a big blue button server.
        /// Uses SHA1 checksum algorithm by default for backward compatibility with BBB 2.x.
        /// </summary>
        /// <param name="host"> The server host </param>
        /// <param name="secret"> The secret used for authentication</param>
        public BigBlueButtonApiBase (string host, string secret) {
            Initialize (host, false);
            Secret = secret;
            HashAlgorithm = ChecksumHashAlgorithm.SHA1;
        }

        /// <summary>
        /// A constructor utilizing a host as well as a secret to setup a connetion to a big blue button server
        /// </summary>
        /// <param name="host"> The server host </param>
        /// <param name="secret"> The secret used for authentication</param>
        /// <param name="ignoreSslErrors"> An indicator if the connection shall ignore SSL errors like invalid certificates </param>
        public BigBlueButtonApiBase (string host, string secret, bool ignoreSslErrors) {
            Initialize (host, ignoreSslErrors);
            Secret = secret;
            HashAlgorithm = ChecksumHashAlgorithm.SHA1;
        }

        /// <summary>
        /// A constructor utilizing a host, secret, and hash algorithm to setup a connection to a big blue button server
        /// </summary>
        /// <param name="host"> The server host </param>
        /// <param name="secret"> The secret used for authentication</param>
        /// <param name="ignoreSslErrors"> An indicator if the connection shall ignore SSL errors like invalid certificates </param>
        /// <param name="hashAlgorithm"> The hash algorithm to use for checksum generation </param>
        public BigBlueButtonApiBase (string host, string secret, bool ignoreSslErrors, ChecksumHashAlgorithm hashAlgorithm) {
            Initialize (host, ignoreSslErrors);
            Secret = secret;
            HashAlgorithm = hashAlgorithm;
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
        /// For more information please have a look into https://docs.bigbluebutton.org/development/api/
        /// </summary>
        /// <param name="name"> The name of the api call </param>
        /// <param name="query"> The query string that is passed to the api call</param>
        /// <returns>The generated checksum</returns>
        public string CreateChecksum (string name, string query) {
            var data = Encoding.UTF8.GetBytes ($"{name}{query}{Secret}");
            byte[] hash;
            switch (HashAlgorithm)
            {
                case ChecksumHashAlgorithm.SHA256:
                    using (var sha256 = SHA256.Create()) { hash = sha256.ComputeHash(data); }
                    break;
                case ChecksumHashAlgorithm.SHA384:
                    using (var sha384 = SHA384.Create()) { hash = sha384.ComputeHash(data); }
                    break;
                case ChecksumHashAlgorithm.SHA512:
                    using (var sha512 = SHA512.Create()) { hash = sha512.ComputeHash(data); }
                    break;
                default:
                    using (var sha1 = SHA1.Create()) { hash = sha1.ComputeHash(data); }
                    break;
            }
            return string.Concat (hash.Select (b => b.ToString ("x2")));
        }

        /// <summary>
        /// This function generates the checksum for the big blue button request, this is used as a authorization 
        /// method for each call to the big blue button server.
        /// For more information please have a look into https://docs.bigbluebutton.org/development/api/
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
            //var encoded = HttpUtility.UrlEncode(value.ToString(), Encoding.UTF8);
            var encoded = Uri.EscapeDataString(value.ToString())
                .Replace("%20", "+");
                //.Replace ("\\%28", "(")
                //.Replace ("\\%29", ")")
                //.Replace ("\\%27", "'")
                //.Replace ("\\%21", "!")
                //.Replace ("\\%7E", "~");
            return encoded;
        }

        /// <summary>
        /// This funciton escapes a bool query parameter in a way that big blue button is able to process it
        /// </summary>
        /// <param name="value">The value that needs to be escaped </param>
        /// <returns> The escaped value </returns>
        public static string EncodeQueryParameter (bool? value) {
            return HttpUtility.UrlEncode (Convert.ToString(value).ToLower(), Encoding.UTF8);
        }

        /// <summary>
        /// This funciton escapes a long query parameter in a way that big blue button is able to process it
        /// </summary>
        /// <param name="value">The value that needs to be escaped </param>
        /// <returns> The escaped value </returns>
        public static string EncodeQueryParameter (long? value) {
            return HttpUtility.UrlEncode (Convert.ToString(value).ToLower(), Encoding.UTF8);
        }
    }
}