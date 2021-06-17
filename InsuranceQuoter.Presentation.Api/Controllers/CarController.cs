namespace InsuranceQuoter.Presentation.Api.Controllers
{
    using System;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using Microsoft.AspNetCore.Mvc;

    public class CarController : Controller
    {
        [Route("Car/{registration}")]
        public IActionResult Get(string registration) =>
            Ok(
                new CarResponse
                {
                    Uid = Guid.NewGuid(),
                    Make = "Ford",
                    Model = "Fiesta",
                    Fuel = "Petrol",
                    Transmission = "Manual",
                    Year = 2015,
                    Mileage = 50000
                });
    }
}