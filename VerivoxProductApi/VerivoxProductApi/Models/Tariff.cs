namespace VerivoxProductApi.Models
{
    public abstract class Tariff
    {
        public string? Name { get; set; }
        public abstract decimal CalculateAnnualCost(decimal consumption);
    }
}
