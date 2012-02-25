using System;

namespace Hospital.Commands
{
    public class DischargePatient : ICommand
    {

        public Guid CommandId { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset When { get; set; }

        public override string ToString()
        {
            return "Discharge Patient";
        }

    }
}
