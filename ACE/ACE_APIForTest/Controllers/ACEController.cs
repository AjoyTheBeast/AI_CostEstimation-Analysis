using ACE_APIForTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACE_APIForTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ACEController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public ACEController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("EstimateCost")]
        public IActionResult EstimateCost(ACEDTOModel item)
        {
            ACEModel model = new ACEModel();
           if(item.UserInput != "")
            {
                model.UserInput = "success";
                model.MLOutput = "88000 24";
            }
            return Ok(model);
        }
    }
}