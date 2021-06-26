namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Providers;
    using Newtonsoft.Json;

    public class PolicyEffects
    {
        private readonly HttpClient httpClient;
        private readonly HostNameProvider hostNameProvider;

        public PolicyEffects(HttpClient httpClient, HostNameProvider hostNameProvider)
        {
            this.httpClient = httpClient;
            this.hostNameProvider = hostNameProvider;
        }

        [EffectMethod]
        public async Task Handle(PoliciesRequestedAction action, IDispatcher dispatcher)
        {
            var url = $"{hostNameProvider.PresentationApiHost}/Policies/{action.UserName}";

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