using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomerInvitation.Models;
using CustomerInvitation.Service.Interfaces;
using Newtonsoft.Json;

namespace CustomerInvitation.Service
{
    public class Invitation : IInvitation
    {
        private GeographicCoordinates partyLocation => new GeographicCoordinates()
        {
            Latitude = 53.339428,
            Longitude = -6.257664
        };

        public IFileReader FileReader { get; set; }
        public IGeographicDistance GeographicDistance  { get; set; }

        public Invitation() 
        {
        }
        
        public Invitation(IFileReader fileReader, IGeographicDistance geographicDistance)
        {
            this.FileReader = fileReader;
            this.GeographicDistance = geographicDistance;
        }

        /// <summary>
        /// List the customers to be invited on console
        /// </summary>
        /// <param name="jsonFilePath">json file containing the customers data</param>
        /// <param name="maxDistance">max distance within office location which someone can be invited</param>
        public void ListCustomersToInviteWithinDistance(string jsonFilePath, int maxDistance)
        {
            List<Customer> customersToInvite = this.GetCustomersToInviteWithinDistance(jsonFilePath, maxDistance);
            foreach (var customer in customersToInvite)
            {
                Console.WriteLine($"{customer.Id} {customer.Name}");
            }
        }

        /// <summary>
        /// Retrieve the list of customers to be invited
        /// </summary>
        /// <param name="jsonFilePath">json file containing the customers data</param>
        /// <param name="maxDistance">max distance within office location which someone can be invited</param>
        /// <returns>List of customers to be invited</returns>
        public List<Customer> GetCustomersToInviteWithinDistance(string jsonFilePath, int maxDistance)
        {
            List<Customer> customersWithinDistance = new List<Customer>();
            List<Customer> customers = this.FileReader.ParseJsonFile<Customer>(jsonFilePath);

            foreach (var customer in customers)
            {
                GeographicCoordinates customerLocation = new GeographicCoordinates()
                {
                    Latitude = customer.Latitude,
                    Longitude = customer.Longitude
                };

                double distance = this.GeographicDistance.Calculate(this.partyLocation, customerLocation);
                if (distance <= maxDistance)
                {
                    customersWithinDistance.Add(customer);
                }
            }

            return customersWithinDistance
                .OrderBy(p => p.Id)
                .ToList();
        }
    }
}