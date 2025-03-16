namespace VerivoxProductApi.Models
{
    public class ComparisonResult
    {
        public string TariffName { get; init; }
        public decimal AnnualCost { get; init; }

        public ComparisonResult(string tariffName, decimal annualCost)
        {
            TariffName = tariffName;
            AnnualCost = annualCost;
        }
    }
}
