using System;

namespace Hospital.Events
{
    public class PatientCreated : IEvent
    {

        public Guid EventId { get; set; }
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
