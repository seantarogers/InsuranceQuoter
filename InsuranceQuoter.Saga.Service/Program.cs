namespace InsuranceQuoter.Saga.Service
{
    using System.Diagnostics.CodeAnalysis;
    using InsuranceQuoter.Infrastructure.Constants;
    using Topshelf;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main()
        {
            HostFactory.Run(
                x =>
                {
                    x.Service<IServiceHost>(
                        s =>
                        {
                            s.ConstructUsing(_ => new ServiceHost());
                            s.WhenStarted((pc, hostControl) => pc.Start(hostControl));
                            ServiceConfiguratorExtensions.WhenStopped(s, pc => pc.Stop());
                        });
                    x.RunAsLocalSystem();

                    x.SetDescription(MessagingEndpointConstants.SagaService);
                    x.SetDisplayName(MessagingEndpointConstants.SagaService);
                    x.SetServiceName(MessagingEndpointConstants.SagaService);

                    x.StartAutomaticallyDelayed();
                });
        }
    }
}