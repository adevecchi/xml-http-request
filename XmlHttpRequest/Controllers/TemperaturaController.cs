using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using TemperatureService;

namespace XmlHttpRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturaController : ControllerBase
    {
        [Route("CelsiusToFahrenheit/{temperature}")]
        [HttpGet]
        public IActionResult CelsiusToFahrenheit(string temperature)
        {
            try
            {
                double result = TemperatureConversion.CelsiusToFahrenheit(temperature);

                return new JsonResult(new { Status = "success", Temperature = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Status = "error", ex.Message });
            }
        }

        [Route("FahrenheitToCelsius/{temperature}")]
        [HttpGet]
        public IActionResult FahrenheitToCelsius(string temperature)
        {
            try {
                double result = TemperatureConversion.FahrenheitToCelsius(temperature);

                return new JsonResult(new { Status = "success", Temperature = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Status = "error", ex.Message });
            }
        }
    }
}
