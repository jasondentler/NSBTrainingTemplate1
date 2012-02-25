using System;

namespace Hospital.Events
{
    public class CommandProcessed : IEvent 
    {

        public CommandProcessed()
        {
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; set; }
        public Guid CommandId { get; set; }

    }
}
