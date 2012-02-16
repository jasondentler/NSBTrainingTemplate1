using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using Newtonsoft.Json;

namespace Hospital.Domain.Specs
{
    public class TestEventDispatcher : IDispatchCommits 
    {
        private readonly List<IEvent> _publishedEvents;

        public TestEventDispatcher(List<IEvent> publishedEvents)
        {
            _publishedEvents = publishedEvents;
        }

        public void Dispose()
        {
        }

        public void Dispatch(Commit commit)
        {
            commit.Events
                .Select(e => e.Body)
                .Cast<IEvent>()
                .ToList()
                .ForEach(Publish);
        }

        private void Publish(IEvent e)
        {
            Console.WriteLine("\t\tEvent {0} {1}", e.GetType().Name, JsonConvert.SerializeObject(e));
            _publishedEvents.Add(e);
        }

    }
}
