using System;
using CommonDomain;
using CommonDomain.Persistence;
using Ninject;
using Ninject.Parameters;

namespace Hospital.CommandHandlers
{
    public class AggregateFactory : IConstructAggregates 
    {
        private readonly IKernel _kernel;

        public AggregateFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            return (IAggregate) _kernel.Get(type, new ConstructorArgument("id", id));
        }
    }
}
