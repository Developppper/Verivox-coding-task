using Microsoft.AspNetCore.Mvc;
using VerivoxProductApi.Interfaces;
using VerivoxProductApi.Services;

namespace VerivoxProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffComparisonService tariffComparisonService;
        private readonly IConfiguration configuration;

        public TariffController(ITariffComparisonService tariffComparisonService, IConfiguration configuration)
        {
            this.tariffComparisonService = tariffComparisonService;
            this.configuration = configuration;
        }

        [HttpGet("compare")]
        public IActionResult Compare([FromQuery] decimal consumption)
        {
            try
            {
                var results = tariffComparisonService.CompareTariffs(consumption);
                return Ok(results);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("products")]
        public IActionResult GetTariffs()
        {
            var filePath = configuration["TariffFile:Path"] ?? "Data/tariffs.json";
            var json = System.IO.File.ReadAllText(filePath);
            return Content(json, "application/json");
        }
    }
}
