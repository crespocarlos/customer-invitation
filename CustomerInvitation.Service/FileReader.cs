using System.Collections.Generic;
using System.IO;
using CustomerInvitation.Service.Interfaces;
using CustomerInvitation.Models;
using Newtonsoft.Json;
using System;

namespace CustomerInvitation
{
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Parses the json file content and deserializes into an object
        /// </summary>
        /// <param name="path">json file path</param>
        /// <returns>List of T</returns>
        public List<T> ParseJsonFile<T>(string path) {
            List<T> customers = new List<T>();

            if (string.IsNullOrWhiteSpace(path)) {
                throw new ArgumentException("path cannot be null");
            }

            using (StreamReader myReader = File.OpenText(path)) {
                string line = string.Empty;
                while ((line = myReader.ReadLine()) != null) {
                    T customer = JsonConvert.DeserializeObject<T>(line);
                    customers.Add(customer);
                }
            }

            return customers;
        }
    }
}