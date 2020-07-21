using System.Net;

using Avaco.BigBlueButton.Api.Models.Response;

namespace Avaco.BigBlueButton.Rest
{
    /// <summary>
    /// This class models a response from the big blue button server
    /// </summary>
    /// <typeparam name="TData">The data type containing the return data of the big blue button server</typeparam>
    public class RestApiResponse<TData>
        where TData : BasicResponse
    {
        /// <summary>
        /// The Http status code returned by the big blue button server
        /// </summary>
        public HttpStatusCode StatusCode {get; protected set;}

        /// <summary>
        /// The data object returned by the big blue button server
        /// </summary>
        public TData Data {get; protected set;}

        /// <summary>
        /// This function checks if the request return code of the response data was cosidered as successfull
        /// </summary>
        public bool IsSuccess {
            get{
                return (StatusCode == HttpStatusCode.OK) && (Data?.ReturnCode == "SUCCESS"); 
            }
        }

        /// <summary>
        /// The default constructor taking the status code as well as the data object
        /// </summary>
        /// <param name="statusCode"> The status code of the http request</param>
        /// <param name="data"> The returned data object </param>
        public RestApiResponse(HttpStatusCode statusCode, TData data){
            StatusCode = statusCode;
            Data = data;
        }
    }
}