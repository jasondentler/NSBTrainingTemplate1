using System;
using NServiceBus;
using NServiceBus.ObjectBuilder.Ninject.Config;
using Ninject.Modules;

namespace Hospital.SqlHandlers
{
    public class NServiceBusModule : NinjectModule 
    {
        public override void Load()
        {
            Func<Type, bool> isCommand = t => t.IsAssignableFrom(typeof(ICommand));
            Func<Type, bool> isEvent = t => t.IsAssignableFrom(typeof(IEvent));
            Func<Type, bool> isMessage = t => t.IsAssignableFrom(typeof(IMessage));

            Configure.With()
                .NinjectBuilder(Kernel)
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
