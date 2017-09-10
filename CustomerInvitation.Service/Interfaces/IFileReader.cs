using System.Collections.Generic;

namespace CustomerInvitation.Service.Interfaces
{
    public interface IFileReader
    {
        /// <summary>
        /// Parses the json file content and deserializes into an object
        /// </summary>
        /// <param name="path">json file path</param>
        /// <returns>List of T</returns>
         List<T> ParseJsonFile<T>(string path);
    }
}