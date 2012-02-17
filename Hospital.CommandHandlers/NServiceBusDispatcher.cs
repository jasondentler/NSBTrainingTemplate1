using System;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class NServiceBusDispatcher : IDispatchCommits  
    {
        private readonly IBus _bus;

        public NServiceBusDispatcher(IBus bus)
        {
            _bus = bus;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Dispatch(Commit commit)
        {
            var events = commit.Events.Select(e => (Hospital.IEvent) e.Body).ToArray();
            _bus.Publish(events);
        }
    }
}
