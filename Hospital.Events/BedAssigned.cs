using System;

namespace Hospital.Events
{
    public class BedAssigned
    {

        public Guid PatientId { get; set; }
        public int Bed { get; set; }

    }
}
