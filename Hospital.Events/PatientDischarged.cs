using System;

namespace Hospital.Events
{
    public class PatientDischarged : IEvent
    {

        public Guid EventId { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }
        public int? Bed { get; set; }

    }
}
