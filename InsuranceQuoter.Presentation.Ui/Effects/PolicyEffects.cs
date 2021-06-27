namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Providers;
    using Newtonsoft.Json;

    public class PolicyEffects
    {
        private readonly HttpClient httpClient;
        private readonly HostNameProvider hostNameProvider;
        private readonly AccessTokenExtractor accessTokenExtractor;

        public PolicyEffects(HttpClient httpClient, HostNameProvider hostNameProvider, AccessTokenExtractor accessTokenExtractor)
        {
            this.httpClient = httpClient;
            this.hostNameProvider = hostNameProvider;
            this.accessTokenExtractor = accessTokenExtractor;
        }

        [EffectMethod]
        public async Task Handle(PoliciesRequestedAction action, IDispatcher dispatcher)
        {
            var url = $"{hostNameProvider.PresentationApiHost}/Policies/{action.UserName}";

            string accessToken = await accessTokenExtractor.Extract().ConfigureAwait(false);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new PoliciesRetrievalFailedAction());

                return;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var policiesResponse = JsonConvert.DeserializeObject<PoliciesResponse>(jsonString);

            dispatcher.Dispatch(
                new PoliciesRetrievedAction(policiesResponse.Policies));
        }
    }
}