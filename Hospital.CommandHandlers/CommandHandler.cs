using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonDomain;
using CommonDomain.Persistence;
using Hospital.Events;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public abstract class CommandHandler<T> where T : class, IAggregate 
    {
        private readonly IRepository _repository;
        private readonly IBus _bus;

        protected CommandHandler(IRepository repository, IBus bus)
        {
            _repository = repository;
            _bus = bus;
        }

        protected T Get(Guid commandId)
        {
            return _repository.GetById<T>(commandId);
        }

        protected void Save(T aggregate, ICommand command)
        {
            _repository.Save(aggregate, command.CommandId, SetHeadersOnAggregate);
            PublishCommandProcessed(command);
        }

        private void SetHeadersOnAggregate(IDictionary<string, object> aggregateHeaders)
        {
            RegularHeaders.ToList()
                .ForEach(item => aggregateHeaders[item.Key] = item.Value);
        }

// ReSharper disable ReturnTypeCanBeEnumerable.Local
        private IDictionary<string, string> Headers { get { return _bus.CurrentMessageContext.Headers; }}
// ReSharper restore ReturnTypeCanBeEnumerable.Local

// ReSharper disable ReturnTypeCanBeEnumerable.Local
        private IDictionary<string, string> RegularHeaders
// ReSharper restore ReturnTypeCanBeEnumerable.Local
        {
            get
            {
                return Headers
                    .Where(item => !item.Key.StartsWith("NServiceBus", StringComparison.InvariantCultureIgnoreCase))
                    .ToDictionary(item => item.Key, item => item.Value);
            }
        }

        private void PublishCommandProcessed(ICommand command)
        {
            var publishedEvent = new CommandProcessed()
                                     {
                                         CommandId = command.CommandId
                                     };

            foreach (var header in RegularHeaders)
                publishedEvent.SetHeader(header.Key, header.Value);

            _bus.Publish(publishedEvent);
        }

    }
}
