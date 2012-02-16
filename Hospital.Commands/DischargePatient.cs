using System;

namespace Hospital.Commands
{
    public class DischargePatient
    {

        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }

    }
}
