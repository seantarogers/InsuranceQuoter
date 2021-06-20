namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System;

    public record CarRetrievedAction(string Id, string Make, string Model, int Year, int Mileage, string Fuel, string Transmission, string Registration)
    {
    }
}