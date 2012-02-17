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
            kernel.Load<NServiceBusModule>();
            kernel.Load<DomainModule>();
        }
    }
}
