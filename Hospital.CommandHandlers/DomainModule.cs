using CommonDomain;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Serialization;
using Ninject;
using Ninject.Modules;

namespace Hospital.CommandHandlers
{
    public class DomainModule : NinjectModule 
    {

        public override void Load()
        {
            RegisterSerializer();
            RegisterAggregateConstructor();
            RegisterConflictDetector();
            RegisterDispatcher();
            RegisterEventStore();
            RegisterRepository();
        }

        private void RegisterSerializer()
        {
            Kernel.Bind<ISerialize>()
                .To<JsonSerializer>();
        }

        private void RegisterAggregateConstructor()
        {
            Kernel.Bind<IConstructAggregates>()
                .To<AggregateFactory>();
        }

        private void RegisterConflictDetector()
        {
            Kernel.Bind<IDetectConflicts>()
                .To<PessimisticConflictDetector>();
        }

        private void RegisterDispatcher()
        {
            Kernel.Bind<IDispatchCommits>()
                .To<NServiceBusDispatcher>();
        }

        private void RegisterEventStore()
        {
            var eventStore = Wireup.Init()
                .LogToOutputWindow()
                .UsingSqlPersistence("Hospital.Domain")
                    .InitializeStorageEngine()
                .UsingRegisteredSerializer(Kernel)
                .UsingAsynchronousDispatchScheduler()
                    .DispatchTo(Kernel.Get<IDispatchCommits>())
                .Build();

            Kernel.Bind<IStoreEvents>()
                .ToConstant(eventStore);
        }

        private void RegisterRepository()
        {
            Kernel.Bind<IRepository>()
                .To<EventStoreRepository>();
        }

    }
}
