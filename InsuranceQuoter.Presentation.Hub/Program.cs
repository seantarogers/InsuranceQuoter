namespace InsuranceQuoter.Presentation.Hub
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    class Program
    {
        static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://localhost:9001")
                .Build().Run();
        }
    }
}
