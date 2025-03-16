using VerivoxProductApi.Models;

namespace VerivoxProductApi.Interfaces
{
    /// <summary>
    /// A service used to mock the response from a product API
    /// </summary>
    public interface ITariffProviderMockService
    {
        IEnumerable<Tariff> GetTariffs();
    }
}
