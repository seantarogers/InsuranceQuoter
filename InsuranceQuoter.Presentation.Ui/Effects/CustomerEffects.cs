namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Exceptions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Providers;
    using Newtonsoft.Json;

    public class CustomerEffects
    {
        private readonly HttpClient httpClient;
        private readonly EndpointProvider endpointProvider;
        private readonly AccessTokenExtractor accessTokenExtractor;

        public CustomerEffects(HttpClient httpClient, EndpointProvider endpointProvider, AccessTokenExtractor accessTokenExtractor)
        {
            this.httpClient = httpClient;
            this.endpointProvider = endpointProvider;
            this.accessTokenExtractor = accessTokenExtractor;
        }

        [EffectMethod]
        public async Task Handle(FindAddressSelectedAction action, IDispatcher dispatcher)
        {
            var url = $"{endpointProvider.PresentationApiEndpoint}/Address/{action.PostCode}";

            string accessToken = await accessTokenExtractor.Extract().ConfigureAwait(false);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new AddressRetrievalFailedAction());

                return;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var addressResponse = JsonConvert.DeserializeObject<AddressResponse>(jsonString);

            if (addressResponse == null)
            {
                throw new AddressCannotBeDeserializedException();
            }

            dispatcher.Dispatch(new AddressesRetrievedAction(addressResponse.Addresses));
        }
    }
}