using System;
using CommonDomain.Persistence;
using NServiceBus;
using NServiceBus.ObjectBuilder;
using Ninject;
using Ninject.Modules;

namespace Hospital.CommandHandlers
{
    public class NServiceBusModule : NinjectModule 
    {
        public override void Load()
        {
            Func<Type, bool> isCommand = t => t.IsAssignableFrom(typeof (ICommand));
            Func<Type, bool> isEvent = t => t.IsAssignableFrom(typeof (IEvent));
            Func<Type, bool> isMessage = t => t.IsAssignableFrom(typeof (IMessage));

            var config = Configure.With()
                .DefaultBuilder()
                .Log4Net()
                .DefiningMessagesAs(isMessage)
                .DefiningCommandsAs(isCommand)
                .DefiningEventsAs(isEvent)
                .RavenSubscriptionStorage()
                .JsonSerializer()
                .MsmqTransport()
                .UnicastBus()
                .LoadMessageHandlers();

            Kernel.Bind<IConfigureComponents>()
                .ToConstant(config.Configurer);

            var bus = config
                .CreateBus()
                .Start();

            Kernel.Bind<IBus>()
                .ToConstant(bus);
        }
    }
}
