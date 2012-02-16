using System;

namespace Hospital.Events
{
    public class BedAssigned : IEvent
    {

        public Guid EventId { get; set; }
        public Guid PatientId { get; set; }
        public int Bed { get; set; }

    }
}
