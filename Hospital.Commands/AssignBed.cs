using System;

namespace Hospital.Commands
{
    public class AssignBed 
    {

        public Guid PatientId { get; set; }
        public int Bed { get; set; }

    }
}
