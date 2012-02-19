using System;
using Hospital.Events;
using NServiceBus;
using Ninject.Modules;

namespace Hospital.SqlHandlers
{
    public class NServiceBusModule : NinjectModule 
    {
        public override void Load()
        {
            Func<Type, bool> isMessage = t => typeof(IMessage).IsAssignableFrom(t);
            Func<Type, bool> isCommand = t => typeof(ICommand).IsAssignableFrom(t);
            Func<Type, bool> isEvent = t => typeof(IEvent).IsAssignableFrom(t);

            var bus = Configure.With()
                .DefaultBuilder()
                .Log4Net()
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

            Kernel.Bind<IBus>().ToConstant(bus);

            //bus.Subscribe<PatientCreated>();
            //bus.Subscribe<PatientAdmitted>();
            //bus.Subscribe<BedAssigned>();
            //bus.Subscribe<PatientMoved>();
            //bus.Subscribe<PatientDischarged>();
        }
    }
}
