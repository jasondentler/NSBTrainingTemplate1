using System;

namespace Hospital.Events
{
    public class PatientDischarged
    {

        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }

    }
}
