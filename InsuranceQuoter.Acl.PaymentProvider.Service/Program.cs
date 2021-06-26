namespace InsuranceQuoter.Acl.PaymentProvider.Service
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

                    x.SetDescription(MessagingEndpointConstants.AclPaymentProviderService);
                    x.SetDisplayName(MessagingEndpointConstants.AclPaymentProviderService);
                    x.SetServiceName(MessagingEndpointConstants.AclPaymentProviderService);

                    x.StartAutomaticallyDelayed();
                });
        }
    }
}