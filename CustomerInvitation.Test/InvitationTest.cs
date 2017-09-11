using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CustomerInvitation;
using CustomerInvitation.Models;
using CustomerInvitation.Service;
using CustomerInvitation.Service.Interfaces;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CustomerInvitation.Test {
    public class InvitationTest {
        private Invitation invitation;

        [Fact]
        public void Should_ReturnInvitations_When_DistanceIsLessThan100() {
            invitation = new Invitation();
    
            var mockFileReader = new Mock<IFileReader>();
            mockFileReader
                .Setup(m => m.ParseJsonFile<Customer>(It.IsAny<string>()))
                .Returns(this.mockCustomers());

            var mockGeographicDistance = new Mock<IGeographicDistance>();
            mockGeographicDistance
                .Setup(m => m.Calculate(It.IsAny<GeographicCoordinates>(), It.IsAny<GeographicCoordinates>()))
                .Returns(99.99);
            
            invitation.FileReader = mockFileReader.Object;
            invitation.GeographicDistance = mockGeographicDistance.Object;

            List<Customer> customers = invitation.GetCustomersToInviteWithinDistance(It.IsAny<string>(), 100);

            Assert.Equal(4, customers.Count);
        }

        [Fact]
        public void Should_ReturnInvitations_When_DistanceIsEqualsTo100() {
            invitation = new Invitation();
    
            var mockFileReader = new Mock<IFileReader>();
            mockFileReader
                .Setup(m => m.ParseJsonFile<Customer>(It.IsAny<string>()))
                .Returns(this.mockCustomers());

            var mockGeographicDistance = new Mock<IGeographicDistance>();
            mockGeographicDistance
                .Setup(m => m.Calculate(It.IsAny<GeographicCoordinates>(), It.IsAny<GeographicCoordinates>()))
                .Returns(100);
            
            invitation.FileReader = mockFileReader.Object;
            invitation.GeographicDistance = mockGeographicDistance.Object;

            List<Customer> customers = invitation.GetCustomersToInviteWithinDistance(It.IsAny<string>(), 100);

            Assert.Equal(4, customers.Count);
        }

        [Fact]
        public void Should_NotReturnInvitations_When_DistanceIsBiggerThan100() {
            invitation = new Invitation();
    
            var mockFileReader = new Mock<IFileReader>();
            mockFileReader
                .Setup(m => m.ParseJsonFile<Customer>(It.IsAny<string>()))
                .Returns(this.mockCustomers());

            var mockGeographicDistance = new Mock<IGeographicDistance>();
            mockGeographicDistance
                .Setup(m => m.Calculate(It.IsAny<GeographicCoordinates>(), It.IsAny<GeographicCoordinates>()))
                .Returns(101);

            
            invitation.FileReader = mockFileReader.Object;
            invitation.GeographicDistance = mockGeographicDistance.Object;

            List<Customer> customers = invitation.GetCustomersToInviteWithinDistance(It.IsAny<string>(), 100);

            Assert.Equal(0, customers.Count);
        }

        [Fact]
        public void Should_ReturnCustomersSortedById() {
            invitation = new Invitation();

            var mockFileReader = new Mock<IFileReader>();
            mockFileReader
                .Setup(m => m.ParseJsonFile<Customer>(It.IsAny<string>()))
                .Returns(this.mockCustomers());

            var mockGeographicDistance = new Mock<IGeographicDistance>();
            mockGeographicDistance
                .Setup(m => m.Calculate(It.IsAny<GeographicCoordinates>(), It.IsAny<GeographicCoordinates>()))
                .Returns(100);

            invitation.FileReader = mockFileReader.Object;
            invitation.GeographicDistance = mockGeographicDistance.Object;

            List<Customer> customers = invitation.GetCustomersToInviteWithinDistance(It.IsAny<string>(), 100);
            for (int i = 0; i < customers.Count - 1; i++) {
                Assert.True(customers[i].Id < customers[i + 1].Id);
            }
        }

        [Fact]
        public void Should_PrintOutputSortedById () {
            invitation = new Invitation();

            var mockFileReader = new Mock<IFileReader>();
            mockFileReader
                .Setup(m => m.ParseJsonFile<Customer>(It.IsAny<string>()))
                .Returns(this.mockCustomers());

            var mockGeographicDistance = new Mock<IGeographicDistance>();
            mockGeographicDistance
                .Setup(m => m.Calculate(It.IsAny<GeographicCoordinates>(), It.IsAny<GeographicCoordinates>()))
                .Returns(100);

            invitation.FileReader = mockFileReader.Object;
            invitation.GeographicDistance = mockGeographicDistance.Object;

            StringBuilder stringBuilder = new StringBuilder();
            
            using (StringWriter sw = new StringWriter ()) {
                Console.SetOut(sw);

                invitation.ListCustomersToInviteWithinDistance(It.IsAny<string>(), 100);

                string expected = "1 David Behan\n8 Eoin Ahearn\n12 Christina McArdle\n39 Lisa Ahearn\n";

                Assert.Equal(expected, sw.ToString());
            }
        }

        private List<Customer> mockCustomers() {
            return new List<Customer>() {
                new Customer() {
                    Id = 12,
                    Name = "Christina McArdle",
                    Latitude = 52.986375,
                    Longitude = -6.043701
                },
                new Customer() {
                    Id = 8,
                    Name = "Eoin Ahearn",
                    Latitude = 54.0894797,
                    Longitude = -6.043701
                },
                new Customer() {
                    Id = 39,
                    Name = "Lisa Ahearn",
                    Latitude = 53.0033946,
                    Longitude = -6.3877505
                },
                new Customer() {
                    Id = 1,
                    Name = "David Behan",
                    Latitude = 52.833502,
                    Longitude = -8.522366
                }
            };
        }
    }
}