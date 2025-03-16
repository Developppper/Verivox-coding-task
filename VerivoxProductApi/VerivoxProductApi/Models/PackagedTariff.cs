namespace VerivoxProductApi.Models
{
    public class PackagedTariff : Tariff
    {
        public PackagedTariff(string name, decimal baseCost, decimal includedKwh, decimal costPerKwh)
        {
            Name = name;
            BaseCost = baseCost;
            IncludedKwh = includedKwh;
            CostPerKwh = costPerKwh;
        }

        public decimal BaseCost { get; }
        public decimal IncludedKwh { get; }
        public decimal CostPerKwh { get; }

        public override decimal CalculateAnnualCost(decimal consumption)
        => BaseCost + Math.Max(0, (consumption - IncludedKwh) * CostPerKwh / 100);
    }
}
