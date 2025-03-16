namespace VerivoxProductApi.Models
{
    public class BasicTariff : Tariff
    {
        public BasicTariff(string name, decimal baseMonthlyCost, decimal costPerKwh)
        {
            Name = name;
            BaseMonthlyCost = baseMonthlyCost;
            CostPerKwh = costPerKwh;
        }

        public decimal BaseMonthlyCost { get; }
        public decimal CostPerKwh { get; }

        public override decimal CalculateAnnualCost(decimal consumption)
            => (BaseMonthlyCost * 12) + (consumption * CostPerKwh / 100);
    }
}
