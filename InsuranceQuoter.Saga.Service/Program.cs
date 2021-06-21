namespace InsuranceQuoter.Saga.Service
{
    using System.Diagnostics.CodeAnalysis;
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

                    x.SetDescription("InsuranceQuote.Saga.Service");
                    x.SetDisplayName("InsuranceQuote.Saga.Service");
                    x.SetServiceName("InsuranceQuote.Saga.Service");

                    x.StartAutomaticallyDelayed();
                });
        }
    }
}