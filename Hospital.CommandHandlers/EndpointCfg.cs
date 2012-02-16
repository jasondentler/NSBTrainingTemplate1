using System;
using NServiceBus;
using NServiceBus.ObjectBuilder.Ninject.Config;
using Ninject;

namespace Hospital.CommandHandlers
{
    public class EndpointCfg : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var kernel = new StandardKernel();

            Func<Type, bool> isCommand = t => t.IsAssignableFrom(typeof(ICommand));
            Func<Type, bool> isEvent = t => t.IsAssignableFrom(typeof(IEvent));
            Func<Type, bool> isMessage = t => t.IsAssignableFrom(typeof(IMessage));

            Configure.With()
                .NinjectBuilder(kernel)
                .DefiningMessagesAs(isMessage)
                .DefiningCommandsAs(isCommand)
                .DefiningEventsAs(isEvent)
                .JsonSerializer()
                .MsmqTransport()
                .MsmqSubscriptionStorage()
                .UnicastBus()
                .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}
