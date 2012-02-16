using System;

namespace Hospital.Events
{
    public class PatientAdmitted : IEvent
    {

        public Guid EventId { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }

    }
}
