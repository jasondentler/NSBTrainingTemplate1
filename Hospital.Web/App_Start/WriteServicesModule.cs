using System;
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

            Func<Type, bool> isCommand = t => t.IsAssignableFrom(typeof(ICommand));
            Func<Type, bool> isEvent = t => t.IsAssignableFrom(typeof(IEvent));
            Func<Type, bool> isMessage = t => t.IsAssignableFrom(typeof(IMessage));
            
            Configure.WithWeb()
                .DefineEndpointName(typeof (MvcApplication).Namespace)
                .NinjectBuilder(Kernel)
                .DefiningMessagesAs(isMessage)
                .DefiningCommandsAs(isCommand)
                .DefiningEventsAs(isEvent)
                .Log4Net()
                .JsonSerializer()
                .MsmqTransport()
                .UnicastBus()
                .SendOnly();
        }
    }
}