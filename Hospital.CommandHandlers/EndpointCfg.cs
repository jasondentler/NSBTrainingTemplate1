using NServiceBus;
using NServiceBus.ObjectBuilder.Ninject.Config;
using Ninject;

namespace Hospital.CommandHandlers
{
    public class EndpointCfg : IWantCustomInitialization
    {
        public void Init()
        {
            var kernel = new StandardKernel();

            Configure.With()
                .NinjectBuilder(kernel)
                .Log4Net()
                .JsonSerializer()
                .UnicastBus()
                .LoadMessageHandlers()
                .CreateBus()
                .Start();
        }
    }
}
