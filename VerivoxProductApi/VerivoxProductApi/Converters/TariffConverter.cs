using System.Text.Json.Serialization;
using System.Text.Json;
using VerivoxProductApi.Models;

namespace VerivoxProductApi.Converters
{
    /// <summary>
    /// Custom JSON converter for polymorphic Tariff deserialization
    /// 
    /// Handles conversion between JSON data and Tariff class hierarchy by:
    /// 1. Inspecting the "Type" property in JSON to determine concrete tariff type
    /// 2. Validating required properties for each tariff type
    /// 3. Creating appropriate Tariff subclass (BasicTariff/PackagedTariff)
    /// 4. Throwing detailed errors for invalid/missing data
    /// </summary>
    public class TariffConverter : JsonConverter<Tariff>
    {
        /// <summary>
        /// Reads and converts JSON to Tariff objects
        /// </summary>
        public override Tariff Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            // Validate required fields exist first
            var missingProps = new List<string>();

            if (!root.TryGetProperty("Type", out var typeProp))
                missingProps.Add("Type");

            if (!root.TryGetProperty("Name", out var nameProp))
                missingProps.Add("Name");

            if (missingProps.Any())
                throw new JsonException($"Missing required properties: {string.Join(", ", missingProps)}");

            var type = typeProp.GetString();
            var name = nameProp.GetString() ?? string.Empty;

            return type switch
            {
                "Basic" => CreateBasicTariff(root, missingProps),
                "Packaged" => CreatePackagedTariff(root, missingProps),
                _ => throw new JsonException($"Unknown tariff type: {type}")
            };
        }

        private BasicTariff CreateBasicTariff(JsonElement root, List<string> missingProps)
        {
            var required = new[] { "BaseMonthlyCost", "CostPerKwh" };
            CheckProperties(root, required, missingProps);

            return new BasicTariff(
                root.GetProperty("Name").GetString(),
                root.GetProperty("BaseMonthlyCost").GetDecimal(),
                root.GetProperty("CostPerKwh").GetDecimal()
            );
        }

        private PackagedTariff CreatePackagedTariff(JsonElement root, List<string> missingProps)
        {
            var required = new[] { "BaseCost", "IncludedKwh", "CostPerKwh" };
            CheckProperties(root, required, missingProps);

            return new PackagedTariff(
                root.GetProperty("Name").GetString(),
                root.GetProperty("BaseCost").GetDecimal(),
                root.GetProperty("IncludedKwh").GetDecimal(),
                root.GetProperty("CostPerKwh").GetDecimal()
            );
        }

        private void CheckProperties(JsonElement root, string[] required, List<string> missingProps)
        {
            foreach (var prop in required)
            {
                if (!root.TryGetProperty(prop, out _))
                    missingProps.Add(prop);
            }

            if (missingProps.Any())
                throw new JsonException($"Missing required properties: {string.Join(", ", missingProps)}");
        }

        /// <summary>
        /// Write method not implemented as this converter is currently only used for deserialization
        /// </summary>
        public override void Write(
            Utf8JsonWriter writer,
            Tariff value,
            JsonSerializerOptions options) =>
            throw new NotImplementedException();
    }
}
