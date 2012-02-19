using NServiceBus;
using Ninject;

namespace Hospital.SqlHandlers
{
    public class EndpointCfg : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            log4net.Config.XmlConfigurator.Configure();

            var kernel = new StandardKernel();
            kernel.Load<NServiceBusModule>();
        }
    }
}
