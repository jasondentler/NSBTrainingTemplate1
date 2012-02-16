using System;

namespace Hospital.Commands
{
    public class CreatePatient
    {

        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
