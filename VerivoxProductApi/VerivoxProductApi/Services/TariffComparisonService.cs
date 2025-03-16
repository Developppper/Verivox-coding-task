using VerivoxProductApi.Interfaces;
using VerivoxProductApi.Models;

namespace VerivoxProductApi.Services
{
    public class TariffComparisonService : ITariffComparisonService
    {
        private readonly ITariffProviderMockService tariffProviderMockService;

        public TariffComparisonService(ITariffProviderMockService tariffProviderMockService)
        {
            this.tariffProviderMockService = tariffProviderMockService;
        }

        public IEnumerable<ComparisonResult> CompareTariffs(decimal consumption)
        {
            if (consumption < 0)
                throw new ArgumentException("Consumption must be non-negative", nameof(consumption));

            return tariffProviderMockService.GetTariffs()
               .Select(t => new ComparisonResult(
                   tariffName: t.Name,
                   annualCost: Math.Round(t.CalculateAnnualCost(consumption), 2)
               ))
               .OrderBy(r => r.AnnualCost);
        }
    }
}
