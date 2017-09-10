using CustomerInvitation.Models;

namespace CustomerInvitation.Service.Interfaces
{
    public interface IGeographicDistance
    {
        /// <summary>
        /// Calculates the geographic distance from one point to another
        /// </summary>
        /// <param name="destination">destination coordinates</param>
        /// <param name="currentLocation">current location coordinates</param>
        /// <returns>distance from current location to destination in kilometers</returns>
         double Calculate(GeographicCoordinates destination, GeographicCoordinates currentLocation);
    }
}