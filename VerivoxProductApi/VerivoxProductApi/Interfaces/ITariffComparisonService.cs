using VerivoxProductApi.Models;

namespace VerivoxProductApi.Interfaces
{
    /// <summary>
    /// A service used to compare the tariffs based on consumption
    /// </summary>
    public interface ITariffComparisonService
    {
        IEnumerable<ComparisonResult> CompareTariffs(decimal consumption);
    }
}
