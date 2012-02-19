using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using SignalR.Infrastructure;

namespace Hospital.Web.App_Start
{
    public class SignalRDependencyResolver : IDependencyResolver
    {
        
        private readonly IKernel _kernel;

        public SignalRDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void Register(Type serviceType, Func<object> activator)
        {
            _kernel.Unbind(serviceType);
            _kernel.Bind(serviceType)
                .ToMethod(ctx => activator());
        }

        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            activators
                .ToList()
                .ForEach(a => _kernel.Bind(serviceType)
                                  .ToMethod(ctx => a()));
        }
    }
}