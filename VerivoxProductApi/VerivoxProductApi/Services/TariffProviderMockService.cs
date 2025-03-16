using System.Text.Json;
using VerivoxProductApi.Converters;
using VerivoxProductApi.Interfaces;
using VerivoxProductApi.Models;

namespace VerivoxProductApi.Services
{
    public class TariffProviderMockService : ITariffProviderMockService
    {
        private readonly IConfiguration configuration;

        public TariffProviderMockService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Tariff> GetTariffs()
        {
            var filePath = configuration["TariffFile:Path"] ?? "Data/tariffs.json";
            var json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new TariffConverter() }
            };

            return JsonSerializer.Deserialize<List<Tariff>>(json, options)
                ?? throw new InvalidOperationException("Failed to deserialize tariffs");
        }
    }
}
