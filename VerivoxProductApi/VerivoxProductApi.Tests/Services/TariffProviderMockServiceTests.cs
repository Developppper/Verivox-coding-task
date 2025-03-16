using Moq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VerivoxProductApi.Interfaces;
using VerivoxProductApi.Services;
using VerivoxProductApi.Models;
using Xunit;


namespace VerivoxProductApi.Tests.Services
{
    public class TariffProviderMockServiceTests
    {
        private readonly string testDataPath;

        public TariffProviderMockServiceTests()
        {
            // Get the test execution directory
            testDataPath = Path.Combine(AppContext.BaseDirectory, "TestData");
            // Ensure test directory exists
            Directory.CreateDirectory(testDataPath);
        }



        private IConfiguration CreateConfig(string filePath)
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"TariffFile:Path", filePath}
                })
                .Build();
        }

        [Fact]
        public void GetTariffs_ValidFile_ReturnsCorrectTariffs()
        {
            var invalidFilePath = Path.Combine(testDataPath, "valid_tariffs.json");
            var config = CreateConfig(invalidFilePath);
            var service = new TariffProviderMockService(config);

            var tariffs = service.GetTariffs().ToList();

            Assert.Equal(2, tariffs.Count);
            Assert.IsType<BasicTariff>(tariffs[0]);
            Assert.IsType<PackagedTariff>(tariffs[1]);
        }

        [Fact]
        public void GetTariffs_MissingFile_ThrowsFileNotFoundException()
        {
            var nonExistentFilePath = Path.Combine(testDataPath, "nonexistent.json");
            var config = CreateConfig(nonExistentFilePath);
            var service = new TariffProviderMockService(config);

            Assert.Throws<FileNotFoundException>(() => service.GetTariffs());
        }

        [Fact]
        public void GetTariffs_InvalidJson_ThrowsJsonException()
        {
            var invalidFilePath = Path.Combine(testDataPath, "invalid.json");
            var config = CreateConfig(invalidFilePath);
            var service = new TariffProviderMockService(config);

            Assert.Throws<JsonException>(() => service.GetTariffs());
        }

        [Fact]
        public void GetTariffs_MissingRequiredProperty_ThrowsJsonException()
        {
            var missingPropertyFilePath = Path.Combine(testDataPath, "missing_property.json");
            var config = CreateConfig(missingPropertyFilePath);

            var service = new TariffProviderMockService(config);

            Assert.Throws<JsonException>(() => service.GetTariffs());
        }
    }
}