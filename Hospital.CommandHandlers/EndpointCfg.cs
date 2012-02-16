using NServiceBus;
using NServiceBus.ObjectBuilder.Ninject.Config;
using Ninject;

namespace Hospital.CommandHandlers
{
    public class EndpointCfg : IConfigureThisEndpoint, IWantCustomInitialization
    {
        public void Init()
        {
            var kernel = new StandardKernel();

            Configure.With()
                .NinjectBuilder(kernel)
                .MsmqTransport()
                .UnicastBus()
                .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}
