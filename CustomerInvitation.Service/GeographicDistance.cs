using System;
using CustomerInvitation.Service.Interfaces;
using CustomerInvitation.Models;

namespace CustomerInvitation
{
    public class GeographicDistance : IGeographicDistance
    {
        private const int EARTH_RADIUS = 6371;

        private static double DegreeToRadian(double angle) => Math.PI * angle / 180.0;
        
        /// <summary>
        /// Calculates the geographic distance from one point to another
        /// </summary>
        /// <param name="destination">destination coordinates</param>
        /// <param name="currentLocation">current location coordinates</param>
        /// <returns>distance from current location to destination in kilometers</returns>
        public double Calculate(GeographicCoordinates destination, GeographicCoordinates currentLocation) 
        {
            if (destination == null || currentLocation == null) {
                throw new ArgumentNullException("destination or currentLocation cannot be null");
            }

            GeographicCoordinates destinationInRadians = new GeographicCoordinates() {
                Longitude = DegreeToRadian(destination.Longitude),
                Latitude = DegreeToRadian(destination.Latitude)
            };

            GeographicCoordinates currentLocationInRadians = new GeographicCoordinates() {
                Longitude = DegreeToRadian(currentLocation.Longitude),
                Latitude = DegreeToRadian(currentLocation.Latitude)
            };

            double longiduteDifference = Math.Abs(destinationInRadians.Longitude - currentLocationInRadians.Longitude);

            double centerAngle = Math.Acos(
                Math.Sin(destinationInRadians.Latitude) * Math.Sin(currentLocationInRadians.Latitude) +
                (Math.Cos(destinationInRadians.Latitude) * Math.Cos(currentLocationInRadians.Latitude) * Math.Cos(longiduteDifference))
            );

            return double.IsNaN(centerAngle) ? 
                0 : 
                Math.Round(centerAngle * EARTH_RADIUS, 2);
        }
    }
}
