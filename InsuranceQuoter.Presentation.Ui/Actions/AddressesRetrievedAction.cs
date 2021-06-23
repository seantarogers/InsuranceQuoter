namespace InsuranceQuoter.Presentation.Ui.Actions
{
    using System.Collections.Generic;
    using InsuranceQuoter.Message.Dtos;

    public record AddressesRetrievedAction(List<AddressDto> Addresses)
    {
    }
}