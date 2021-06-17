namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System;

    public record CarRetrievedAction(Guid Uid, string Make, string Model, int Year, int Mileage, string Fuel, string Transmission, string Registration)
    {
    }
}