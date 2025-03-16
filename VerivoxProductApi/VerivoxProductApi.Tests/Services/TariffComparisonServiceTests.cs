using Moq;
using VerivoxProductApi.Interfaces;
using VerivoxProductApi.Models;
using VerivoxProductApi.Services;
using Xunit;

namespace VerivoxProductApi.Tests.Services
{
    public class TariffComparisonServiceTests
    {
        private readonly Mock<ITariffProviderMockService> mockProvider = new();
        private readonly TariffComparisonService service;

        public TariffComparisonServiceTests()
        {
            service = new TariffComparisonService(mockProvider.Object);
        }

        [Fact]
        public void CompareTariffs_NegativeConsumption_ThrowsArgumentException()
        {
            var consumption = -100m;

            var ex = Assert.Throws<ArgumentException>(() => service.CompareTariffs(consumption));
            Assert.Equal("Consumption must be non-negative (Parameter 'consumption')", ex.Message);
        }

        [Fact]
        public void CompareTariffs_ValidTariffs_ReturnsSortedResults()
        {
            var mockTariffs = new List<Tariff>
            {
                new BasicTariff("Product A", 5m, 22m),
                new PackagedTariff("Product B", 800m, 4000m, 30m)
            };

            mockProvider.Setup(p => p.GetTariffs()).Returns(mockTariffs);

            var results = service.CompareTariffs(4500m).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal("Product B", results[0].TariffName);
            Assert.Equal(950m, results[0].AnnualCost);
            Assert.Equal("Product A", results[1].TariffName);
            Assert.Equal(1050m, results[1].AnnualCost);
        }

        [Fact]
        public void CompareTariffs_EmptyTariffList_ReturnsEmpty()
        {
            mockProvider.Setup(p => p.GetTariffs()).Returns(new List<Tariff>());

            var results = service.CompareTariffs(5000m);

            Assert.Empty(results);
        }

        [Fact]
        public void CompareTariffs_ZeroConsumption_CalculatesBaseCosts()
        {
            var mockTariffs = new List<Tariff>
            {
                new BasicTariff("Product A", 5m, 22m)
            };

            mockProvider.Setup(p => p.GetTariffs()).Returns(mockTariffs);

            var result = service.CompareTariffs(0m).Single();

            Assert.Equal(60m, result.AnnualCost); // 5€ * 12 months
        }
    }
}