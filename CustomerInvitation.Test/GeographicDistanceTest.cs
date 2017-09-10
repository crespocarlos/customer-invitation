using System;
using CustomerInvitation.Models;
using Xunit;

namespace CustomerInvitation.Test
{
    public class GeographicDistanceTest
    {
        private GeographicDistance geographicDistance;

        [Fact]
        public void Should_CalculateNoDistance_When_CoordenatesAreTheSame() {
            geographicDistance = new GeographicDistance();

            GeographicCoordinates destination = new GeographicCoordinates() {
                Latitude = 53.339428,
                Longitude = -6.257664
            };

            GeographicCoordinates origin = new GeographicCoordinates() {
                Latitude = 53.339428,
                Longitude = -6.257664
            };

            Assert.Equal(0, geographicDistance.Calculate(destination, origin));
        }

        [Fact]
        public void Should_CalculateDistance() {
            geographicDistance = new GeographicDistance();

            GeographicCoordinates destination = new GeographicCoordinates() {
                Latitude = 53.339428,
                Longitude = -6.257664
            };

            GeographicCoordinates origin = new GeographicCoordinates() {
                Latitude = 52.986375,
                Longitude = -6.043701
            };

            Assert.Equal(41.77, geographicDistance.Calculate(destination, origin));
        }

        [Fact]
        public void Should_ThrowException_When_ParameterIsNull() {
            geographicDistance = new GeographicDistance();
            Exception ex = null;

            ex = Assert.Throws<ArgumentNullException>(() => geographicDistance.Calculate(null, new GeographicCoordinates()));

            Assert.NotNull(ex);

            ex = Assert.Throws<ArgumentNullException>(() => geographicDistance.Calculate(new GeographicCoordinates(), null));

            Assert.NotNull(ex);
        }
    }
}