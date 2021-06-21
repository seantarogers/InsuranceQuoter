namespace InsuranceQuoter.Saga.Service
{
    using Topshelf;

    public interface IServiceHost
    {
        bool Start(HostControl topshelfHostControl = null);

        bool Stop();
    }
}