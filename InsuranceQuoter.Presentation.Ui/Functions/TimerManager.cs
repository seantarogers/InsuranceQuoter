namespace InsuranceQuoter.Presentation.Ui.Functions
{
    using System.Timers;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Actions;

    public class TimerManager
    {
        private static int Ticks = 0;
        private static int maxTicks;
        private readonly IDispatcher dispatcher;
        private Timer timer;

        public TimerManager(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void Initialize(int requestedMaxTicks)
        {
            maxTicks = requestedMaxTicks;

            timer?.Stop();

            Ticks = 0;
            timer = new Timer(500) { AutoReset = true };
            timer.Elapsed += OnTick;
            timer.Start();
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            dispatcher.Dispatch(new TimerElapsedAction());
            Ticks++;

            if (Ticks > maxTicks)
            {
                dispatcher.Dispatch(new TimerFinishedAction());
                timer.Stop();
            }
        }
    }
}