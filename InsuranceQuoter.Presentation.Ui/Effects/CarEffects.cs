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

    public class CarEffects
    {
        private readonly HttpClient httpClient;
        private readonly EndpointProvider endpointProvider;
        private readonly AccessTokenExtractor accessTokenExtractor;

        public CarEffects(HttpClient httpClient, EndpointProvider endpointProvider, AccessTokenExtractor accessTokenExtractor)
        {
            this.httpClient = httpClient;
            this.endpointProvider = endpointProvider;
            this.accessTokenExtractor = accessTokenExtractor;
        }

        [EffectMethod]
        public async Task Handle(FindCarSelectedAction action, IDispatcher dispatcher)
        {
            var url = $"{endpointProvider.PresentationApiEndpoint}/Car/{action.RegistrationNumber}";

            string accessToken = await accessTokenExtractor.Extract().ConfigureAwait(false);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new CarNotFoundAction());

                return;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var carResponse = JsonConvert.DeserializeObject<CarResponse>(jsonString);

            if (carResponse == null)
            {
                throw new CarCannotBeDeserializedException();
            }

            dispatcher.Dispatch(
                new CarRetrievedAction(
                    carResponse.Id,
                    carResponse.Make,
                    carResponse.Model,
                    carResponse.Year,
                    carResponse.Mileage,
                    carResponse.Fuel,
                    carResponse.Transmission,
                    action.RegistrationNumber));
        }
    }
}