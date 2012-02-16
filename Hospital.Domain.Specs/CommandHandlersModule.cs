using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.CommandHandlers;
using NServiceBus;
using Ninject;
using Ninject.Modules;

namespace Hospital.Domain.Specs
{
    public class CommandHandlersModule : NinjectModule 
    {
        public override void Load()
        {
            RegisterHandlers(Kernel, typeof(CreatePatientHandler).Assembly.GetTypes());
        }

        private static void RegisterHandlers(IKernel kernel, IEnumerable<Type> types)
        {
            types.Select(impl => new { impl, iface = impl.GetInterfaces() })
                .SelectMany(x => x.iface, (x, iface) => new { x.impl, iface })
                .Where(x => x.iface.IsGenericType)
                .Select(x => new { x.impl, x.iface, genericIface = x.iface.GetGenericTypeDefinition() })
                .Where(x => x.genericIface == typeof(IHandleMessages<>))
                .ToList()
                .ForEach(x => RegisterHandler(kernel, x.impl, x.iface));
        }

        private static void RegisterHandler(IKernel kernel, Type implementation, Type @interface)
        {
            kernel.Bind(@interface)
                .ToMethod(ctx => ctx.Kernel.Get(implementation));
        }

    }
}
