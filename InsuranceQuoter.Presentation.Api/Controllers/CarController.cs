namespace InsuranceQuoter.Presentation.Api.Controllers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using Microsoft.AspNetCore.Mvc;

    public class CarController : Controller
    {
        private readonly IAsyncQueryHandler<GetCarByRegistrationNumberQuery, CarByRegistrationNumberResult> getCarByRegistrationNumberQueryHandler;

        public CarController(IAsyncQueryHandler<GetCarByRegistrationNumberQuery, CarByRegistrationNumberResult> getCarByRegistrationNumberQueryHandler)
        {
            this.getCarByRegistrationNumberQueryHandler = getCarByRegistrationNumberQueryHandler;
        }

        [Route("Car/{registration}")]
        public async Task<IActionResult> Get(string registration)
        {
            if (string.IsNullOrWhiteSpace(registration))
            {
                return BadRequest();
            }

            CarByRegistrationNumberResult result = await getCarByRegistrationNumberQueryHandler.HandleAsync(new GetCarByRegistrationNumberQuery(registration)).ConfigureAwait(false);

            return Ok(
                new CarResponse
                {
                    Id = result.Car.Id,
                    Fuel = result.Car.FuelType,
                    Make = result.Car.Make,
                    Mileage = result.Car.Mileage,
                    Model = result.Car.Model,
                    Transmission = result.Car.TransmissionType,
                    Year = result.Car.YearOfManufacture
                });
        }
    }
}