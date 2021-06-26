namespace InsuranceQuoter.Application.Command.Commands
{
    using System;

    public record AddRiskCommand(
        string AddressLine1,
        string AddressLine2,
        string City,
        string County,
        string Postcode,
        string Model,
        string Transmission,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        Guid AddressUid,
        int Year,
        string Registration,
        string CoverType,
        string Email,
        int Mileage,
        string Fuel,
        string Make) : Command;
}