using System;

namespace Hospital.Commands
{
    public class AdmitPatient  
    {

        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }

    }
}
