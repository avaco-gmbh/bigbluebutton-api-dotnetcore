
using System;
using System.IO;
using System.Threading.Tasks;
using Avaco.BigBlueButton.Helpers.Interfaces;

namespace Avaco.BigBlueButton.Helpers
{   
    /// <inheritdoc/>
    public class FileConversionHelper : IFileConversionHelper
    {       
        public string ToBase64(byte[] buffer) {
            var ret = Convert.ToBase64String(buffer);
            return ret;

        }  

        public async Task<string> ToBase64Async(MemoryStream stream) {
            return await ToBase64Async(stream);
        }

        public async Task<string> ToBase64Async(Stream stream) {
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer,0,buffer.Length);
            return ToBase64(buffer);
        }
    }
}