using NServiceBus;
using Ninject;

namespace Hospital.SqlHandlers
{
    public class EndpointCfg : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var kernel = new StandardKernel();
            kernel.Load<NServiceBusModule>();
        }
    }
}
