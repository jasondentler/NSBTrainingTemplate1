using System;

namespace Hospital.Events
{
    public class PatientAdmitted  
    {

        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }
    }
}
