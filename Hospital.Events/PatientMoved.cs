using System;

namespace Hospital.Events
{
    public class PatientMoved
    {

        public Guid PatientId { get; set; }
        public int FromBed { get; set; }
        public int ToBed { get; set; }

    }
}
