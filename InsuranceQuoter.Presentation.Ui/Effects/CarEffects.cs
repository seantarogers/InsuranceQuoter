namespace InsuranceQuoter.Presentation.Ui.Effects
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using Newtonsoft.Json;

    public class CarEffects
    {
        private readonly HttpClient httpClient;

        public CarEffects(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [EffectMethod]
        public async Task Handle(FindCarSelectedAction action, IDispatcher dispatcher)
        {
            await Task.Delay(500);

            string url = "https://localhost:44307/Car/" + action.RegistrationNumber;

            HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new CarRetrievalFailedAction());

                return;
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var carResponse = JsonConvert.DeserializeObject<CarResponse>(jsonString);

            dispatcher.Dispatch(
                new CarRetrievedAction(
                    carResponse.Uid,
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