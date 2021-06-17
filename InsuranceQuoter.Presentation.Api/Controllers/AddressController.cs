namespace InsuranceQuoter.Presentation.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using InsuranceQuoter.Infrastructure.Message.Dtos;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class AddressController : Controller
    {
        [Route("Address/{postcode}")]
        [HttpGet]
        public IActionResult Get(string postCode) =>
            Ok(
                new AddressResponse()
                {
                    Addresses = new List<AddressDto>()
                    {
                        new AddressDto()
                        {
                            Uid = Guid.NewGuid(),
                            AddressLine1 = "43 Havelock Road",
                            AddressLine2 = "Fiveways",
                            City = "Brighton",
                            County = "Sussex",
                            Postcode = postCode,
                        },
                        new AddressDto()
                        {
                            Uid = Guid.NewGuid(),
                            AddressLine1 = "45 Havelock Road",
                            AddressLine2 = "Fiveways",
                            City = "Brighton",
                            County = "Sussex",
                            Postcode = postCode,
                        },
                        new AddressDto()
                        {
                            Uid = Guid.NewGuid(),
                            AddressLine1 = "47 Havelock Road",
                            AddressLine2 = "Fiveways",
                            City = "Brighton",
                            County = "Sussex",
                            Postcode = postCode,
                        }
                    }
                });
    }
}