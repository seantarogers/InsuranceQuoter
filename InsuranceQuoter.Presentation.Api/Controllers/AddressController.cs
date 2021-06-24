namespace InsuranceQuoter.Presentation.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Message.Dtos;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAsyncQueryHandler<GetAddressesByPostCodeQuery, AddressesByPostcodeResult> getAddressesByPostCodeQueryHandler;

        public AddressController(IAsyncQueryHandler<GetAddressesByPostCodeQuery, AddressesByPostcodeResult> getAddressesByPostCodeQueryHandler)
        {
            this.getAddressesByPostCodeQueryHandler = getAddressesByPostCodeQueryHandler;
        }

        [Route("Address/{postcode}")]
        [HttpGet]
        public async Task<IActionResult> Get(string postCode)
        {
            if (string.IsNullOrWhiteSpace(postCode))
            {
                return BadRequest();
            }

            AddressesByPostcodeResult result = await getAddressesByPostCodeQueryHandler.HandleAsync(new GetAddressesByPostCodeQuery(postCode)).ConfigureAwait(false);

            return
                Ok(
                    new AddressResponse
                    {
                        Addresses = result.Addresses.Select(
                            a => new AddressDto()
                            {
                                AddressLine1 = a.AddressLine1,
                                City = a.City,
                                County = a.County,
                                Postcode = a.Postcode,
                                AddressLine2 = a.AddressLine2,
                                Id = a.Id
                            }).ToList()
                    });
        }
    }
}