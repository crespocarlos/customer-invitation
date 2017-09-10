using System;
using System.Collections.Generic;
using System.IO;
using CustomerInvitation.Models;
using Xunit;

namespace CustomerInvitation.Test
{
    public class FileReaderTest
    {
        private FileReader fileReader;
        private readonly string jsonPath = "../../../Resource/customerListMock.json";

        [Fact]
        public void Should_ThrowException_When_ArgumentIsNullOrEmpty() {
            fileReader = new FileReader();
            Exception ex = Assert.Throws<ArgumentException>(() => fileReader.ParseJsonFile<Customer>(null));

            Assert.NotNull(ex);
        }

        [Fact]
        public void Should_ThrowException_When_FileNotFound() {
            fileReader = new FileReader();
            Exception ex = Assert.Throws<FileNotFoundException> (() => fileReader.ParseJsonFile<Customer>("a"));

            Assert.NotNull(ex);
        }

        [Fact]
        public void Should_ReturnListOfCustomers_When_FileIsParsed() {
            fileReader = new FileReader();
            List<Customer> result = fileReader.ParseJsonFile<Customer>(jsonPath);

            Assert.NotNull(result);
            Assert.Equal(10, result.Count);
        }
    }
}