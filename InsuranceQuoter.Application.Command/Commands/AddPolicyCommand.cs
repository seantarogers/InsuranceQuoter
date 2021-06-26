namespace InsuranceQuoter.Application.Command.Commands
{
    using System;

    public record AddPolicyCommand(
        Guid PolicyUid,
        Guid RiskUid,
        string Email,
        string AddressLine1,
        string Postcode,
        string Model,
        string FirstName,
        string LastName,
        Guid AddressUid,
        string Registration,
        string CoverType,
        string Make,
        string InsurerName,
        string Addons) : Command;
}