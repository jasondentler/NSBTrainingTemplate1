using CommonDomain;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Persistence.InMemoryPersistence;
using Hospital.CommandHandlers;
using Ninject;
using Ninject.Modules;
using TechTalk.SpecFlow;

namespace Hospital.Domain.Specs
{
    public class DomainModule : NinjectModule 
    {
        public override void Load()
        {
            RegisterAggregateConstructor();
            RegisterConflictDetector();
            RegisterEventStore();
            RegisterRepository();
        }

        private void RegisterAggregateConstructor()
        {
            Kernel.Bind<IConstructAggregates>()
                .To<AggregateFactory>()
                .InSingletonScope();
        }

        private void RegisterConflictDetector()
        {
            Kernel.Bind<IDetectConflicts>()
                .To<NullConflictDetector>()
                .InSingletonScope();
        }

        private void RegisterEventStore()
        {
            Kernel.Bind<IStoreEvents>()
                .ToMethod(ctx =>
                {
                    var factory = new InMemoryPersistenceFactory();
                    var persister = factory.Build();
                    var dispatcher = Kernel.Get<IDispatchCommits>();
                    var scheduler = new SynchronousDispatchScheduler(dispatcher, persister);
                    var hook = new DispatchSchedulerPipelineHook(scheduler);
                    var store = new OptimisticEventStore(persister, new[] {hook});
                    return store;
                })
                .InScope(ctx => ScenarioContext.Current);
        }

        private void RegisterRepository()
        {
            Kernel.Bind<IRepository>()
                .ToMethod(ctx => ctx.Kernel.Get<EventStoreRepository>())
                .InScope(ctx => ScenarioContext.Current);
        }
        
    }
}
