﻿namespace InsuranceQuoter.Presentation.Ui.Functions
{
    using System;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using InsuranceQuoter.Presentation.Ui.Actions;
    using InsuranceQuoter.Presentation.Ui.Providers;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalRConnectionManager
    {
        private readonly IDispatcher dispatcher;
        private readonly EndpointProvider endpointProvider;

        public SignalRConnectionManager(IDispatcher dispatcher, EndpointProvider endpointProvider)
        {
            this.dispatcher = dispatcher;
            this.endpointProvider = endpointProvider;
        }

        public static HubConnection HubConnection { get; set; }

        public async Task Initialize()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl($"{endpointProvider.HubEndpoint}/quotehub")
                .Build();

            await HubConnection.StartAsync();

            HubConnection.On(
                "QuoteResponseHandler",
                (QuoteResponse response) =>
                {
                    dispatcher.Dispatch(
                        new QuoteReceivedAction(
                            response.Uid,
                            response.Insurer,
                            response.Premium,
                            response.Addons,
                            response.StartDate,
                            response.PremiumTax));
                });

            HubConnection.On(
                "ReadyToContactPaymentProviderEventHandler",
                (ReadyToContactPaymentProviderEvent _) => { dispatcher.Dispatch(new ReadyToContactPaymentProviderAction()); });

            HubConnection.On(
                "CardPaymentTakenEventHandler",
                (CardPaymentTakenEvent _) => { dispatcher.Dispatch(new CardPaymentTakenAction()); });

            HubConnection.On(
                "ReadyToBindPolicyWithInsurerEventHandler",
                (ReadyToBindPolicyWithInsurerEvent _) => { dispatcher.Dispatch(new ReadyToBindPolicyWithInsurerAction()); });

            HubConnection.On(
                "RiskUidGeneratedEventHandler",
                (RiskUidGeneratedEvent @event) => { dispatcher.Dispatch(new RiskUidGeneratedAction(@event.RiskUid)); });

            HubConnection.On(
                "PolicyBoundEventHandler",
                (PolicyBoundEvent _) => { dispatcher.Dispatch(new PolicyBoundAction()); });

            HubConnection.On(
                "ReadyToStorePolicyEventHandler",
                (ReadyToStorePolicyEvent _) => { dispatcher.Dispatch(new ReadyToStorePolicyAction()); });

            HubConnection.On(
                "PurchaseAndBindCompletedEventHandler",
                (PurchaseAndBindCompletedEvent @event) =>
                {
                    dispatcher.Dispatch(new PolicyPurchaseAndBindCompletedAction(@event.InsurerName, @event.PolicyUid));

                    Task.Delay(TimeSpan.FromMilliseconds(1000)).ContinueWith(
                        _ => { dispatcher.Dispatch(new PurchaseOperationCompletedAction()); }).GetAwaiter().GetResult();
                });
        }
    }
}