namespace InsuranceQuoter.Presentation.Ui.Pages
{
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using Microsoft.AspNetCore.Components;

    public partial class Index
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new InitializeStateAction());

            await base.OnInitializedAsync();
        }
    }
}