using System;

namespace Hospital
{
    public class PatientDetails
    {

        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? BedAssignment { get; set; }
        
        public DateTimeOffset? Admitted { get; set; }
        public DateTimeOffset? Discharged { get; set; }

        public string LastFirst { get { return string.Join(", ", new[] {LastName, FirstName}); } }

        public bool CanAdmit { get; set; }
        public bool CanDischarge { get; set; }
        public bool CanAssignBed { get; set; }

    }
}