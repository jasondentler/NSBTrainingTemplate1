using System;

namespace Hospital
{
    public interface IEvent : IMessage
    {

        Guid EventId { get; }

    }
}
