namespace InsuranceQuoter.Presentation.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using Microsoft.AspNetCore.Mvc;

    public class PolicyController : Controller
    {
        private readonly IAsyncQueryHandler<GetPoliciesByUserNameQuery, PoliciesByUserNameResult> getPoliciesByUserNameQueryHandler;

        public PolicyController(IAsyncQueryHandler<GetPoliciesByUserNameQuery, PoliciesByUserNameResult> getPoliciesByUserNameQueryHandler)
        {
            this.getPoliciesByUserNameQueryHandler = getPoliciesByUserNameQueryHandler;
        }

        [Route("Policies/{userName}")]
        public async Task<IActionResult> Get(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest();
            }

            PoliciesByUserNameResult result = await getPoliciesByUserNameQueryHandler.HandleAsync(new GetPoliciesByUserNameQuery(username)).ConfigureAwait(false);

            return Ok(
                new PoliciesResponse
                {
                    Policies = result.Policies.ToList()
                });
        }
    }
}