using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using NServiceBus;
using Newtonsoft.Json;
using Ninject;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Hospital.Domain.Specs
{
    [Binding]
    public class DomainContext
    {

        private static readonly IKernel Kernel = new StandardKernel();

        static DomainContext()
        {
            Kernel.Bind<IKernel>().ToConstant(Kernel);
            Kernel.Load<DomainModule>();
            Kernel.Load<CommandHandlersModule>();
        }

        private readonly Dictionary<Guid, List<IEvent>> _givenEvents = new Dictionary<Guid, List<IEvent>>();
        private readonly List<IEvent> _publishedEvents = new List<IEvent>();
        private Exception _exception;
        private readonly HashSet<IEvent> _checkedEvents = new HashSet<IEvent>();

        [BeforeScenario("domain")]
        public void BeforeDomain()
        {
            Kernel.Rebind<IDispatchCommits>()
                .ToConstant(new TestDispatcher(_publishedEvents));

            ScenarioContext.Current.Set(this);
        }

        public IEnumerable<IEvent> GivenEvents(Guid aggregateId)
        {
            if (_givenEvents.ContainsKey(aggregateId))
            {
                return _givenEvents[aggregateId].AsEnumerable();
            }
            return new IEvent[0];
        }

        public IEnumerable<IEvent> PublishedEvents { get { return _publishedEvents.AsEnumerable(); } }
        public Exception Exception { get { return _exception; } }

        public void Given(Guid aggregateId, IEvent @event)
        {
            Console.WriteLine("\t\tEvent {0} {1}", @event.GetType().Name, JsonConvert.SerializeObject(@event));

            var store = Kernel.Get<IStoreEvents>();
            var stream = store.OpenStream(aggregateId, 0, int.MaxValue);
            stream.Add(new EventMessage()
                           {
                               Body = @event
                           });
            stream.CommitChanges(Guid.NewGuid());

            if (!_givenEvents.ContainsKey(aggregateId))
                _givenEvents[aggregateId] = new List<IEvent>();
            _givenEvents[aggregateId].Add(@event);

        }

        public void When<TCommand>(TCommand command) where TCommand : ICommand
        {
            Console.WriteLine("\tCommand {0} {1}", command.GetType().Name, JsonConvert.SerializeObject(command));
            var handlerType = typeof (IHandleMessages<TCommand>);
            dynamic handler = Kernel.TryGet(handlerType);

            if (handler == null)
                throw new ApplicationException(string.Format("No handler for {0}", typeof (TCommand)));

            try
            {
                handler.Handle(command);
            }
            catch (Exception exception)
            {
                Console.WriteLine("\t\t{0} {1}", exception.GetType().Name, exception.ToString());
                _exception = exception;
            }
        }

        public TEvent Then<TEvent>() where TEvent : IEvent
        {
            return Then<TEvent>(t => true);
        }

        public TEvent Then<TEvent>(Func<TEvent, bool> filter) where TEvent : IEvent
        {
            var e = PublishedEvents.OfType<TEvent>()
                .Where(filter)
                .Single();
            MarkChecked(e);
            return e;
        }
        
        public void MarkChecked(IEvent e)
        {
            _checkedEvents.Add(e);
        }
        
        [Then(@"nothing happens"), Scope(Tag="domain")]
        public void ThenNothingHappens()
        {
            _publishedEvents.Should().Be.Empty();
            _exception.Should().Be.Null();
        }

        [Then(@"nothing else happens"), Scope(Tag="domain")]
        public void ThenNothingElseHappens()
        {
            _publishedEvents.Except(_checkedEvents).Should().Be.Empty();
            _exception.Should().Be.Null();
        }


        [Then(@"error: (.+)")]
        public void ThenError(string message)
        {
            _exception.Should().Not.Be.Null();
            _exception.Message.Should().Be.EqualTo(message);
        }

        
    }
}
