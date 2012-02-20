using System;
using Hospital.Web.Signalr;
using NServiceBus;
using NServiceBus.ObjectBuilder.Ninject.Config;
using Hospital.WriteModel;
using Ninject.Modules;

namespace Hospital.Web.App_Start
{
    public class WriteServicesModule : NinjectModule 
    {
        public override void Load()
        {

            Kernel.Bind<IHospitalWriteService>()
                .To<HospitalWriteService>()
                .InSingletonScope();

            var ensureTheCompilerDoesntOptimizeThisReferenceAway = typeof (StatsHandler);

            Func<Type, bool> isMessage = t => typeof(IMessage).IsAssignableFrom(t);
            Func<Type, bool> isCommand = t => typeof(ICommand).IsAssignableFrom(t);
            Func<Type, bool> isEvent = t => typeof(IEvent).IsAssignableFrom(t);


            Configure.WithWeb()
                .NinjectBuilder(Kernel)
                .DefineEndpointName("Hospital.Web")
                .DefiningMessagesAs(isMessage)
                .DefiningCommandsAs(isCommand)
                .DefiningEventsAs(isEvent)
                .Log4Net()
                .JsonSerializer()
                .MsmqTransport()
                .UnicastBus()
                .LoadMessageHandlers()
                .PurgeOnStartup(false)
                .CreateBus()
                .Start();

        }

    }
}