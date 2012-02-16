using System;

namespace Hospital.Commands
{
    public class AssignBed : ICommand
    {

        public Guid CommandId { get; set; }
        public Guid PatientId { get; set; }
        public int Bed { get; set; }

    }
}
