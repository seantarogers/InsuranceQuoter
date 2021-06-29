namespace InsuranceQuoter.Presentation.Ui.Actions
{
    public record CarRetrievedAction(string Id, string Make, string Model, int Year, int Mileage, string Fuel, string Transmission, string Registration);
}