namespace InsuranceQuoter.Application.Command.Handlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Command.Commands;
    using InsuranceQuoter.Application.Command.Results;

    public interface IAsyncCommandHandler<in TCommand, out TCommandResult>
        where TCommand : Command where TCommandResult : CommandResult
    {
        TCommandResult Result { get; }

        Task HandleAsync(TCommand command);
    }
}