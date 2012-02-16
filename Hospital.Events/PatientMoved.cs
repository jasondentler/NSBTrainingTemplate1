using System;

namespace Hospital.Events
{
    public class PatientMoved : IEvent
    {

        public Guid EventId { get; set; }
        public Guid PatientId { get; set; }
        public int FromBed { get; set; }
        public int ToBed { get; set; }

    }
}
