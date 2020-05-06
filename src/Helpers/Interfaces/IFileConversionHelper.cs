using System.IO;
using System.Threading.Tasks;

namespace Avaco.BigBlueButton.Helpers.Interfaces
{

    /// <summary>
    /// This interface defines methods to convert file data to Base64 string
    /// </summary>
    public interface IFileConversionHelper
    {
        /// <summary>
        /// This method converts a byte[] to a base64 string
        /// </summary>
        /// <param name="buffer"> The byte[] that is supposed to be converted to base64 </param>
        /// <returns> The converted base64 string </returns>
        string ToBase64(byte[] buffer);
        /// <summary>
        /// This method converts aMemoryStream to a base64 string
        /// </summary>
        /// <param name="buffer"> The MemoryStream that is supposed to be converted to base64 </param>
        /// <returns> The converted base64 string </returns>
        Task<string> ToBase64Async(MemoryStream stream); 

        /// <summary>
        /// This method converts a Stream to a base64 string
        /// </summary>
        /// <param name="buffer"> The Stream that is supposed to be converted to base64 </param>
        /// <returns> The converted base64 string </returns>
        Task<string> ToBase64Async(Stream stream);
    }
}