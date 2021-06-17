namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using Microsoft.AspNetCore.Components;

    public partial class Index
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Inject]
        public SignalRConnectionManager SignalRConnectionManager { get;set; }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new InitializeStateAction());

#if DEBUG
            await Task.Delay(10000);
#endif
            await SignalRConnectionManager.Initialize();
            
            await base.OnInitializedAsync();
        }
    }
}