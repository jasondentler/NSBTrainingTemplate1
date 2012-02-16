using System;

namespace Hospital.Commands
{
    public class CreatePatient : ICommand
    {

        public Guid CommandId { get; set; }
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
