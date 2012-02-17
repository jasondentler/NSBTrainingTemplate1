using CommonDomain.Persistence;
using NServiceBus;
using NServiceBus.ObjectBuilder;
using Ninject;

namespace Hospital.CommandHandlers
{
    public class EndpointCfg : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            var kernel = new StandardKernel();
            kernel.Load<NServiceBusModule>();
            kernel.Load<DomainModule>();

            var nsbContainer = kernel.Get<IConfigureComponents>();
            nsbContainer.RegisterSingleton<IRepository>(kernel.Get<IRepository>());
        }
    }
}
