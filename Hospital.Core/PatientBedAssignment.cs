using System;

namespace Hospital
{
    public class PatientBedAssignment
    {

        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? BedAssignment { get; set; }
        public bool IsAdmitted { get; set; }
        public bool IsDischarged { get; set; }

        public string LastFirst { get { return string.Join(", ", new[] { LastName, FirstName }); } }

    }
}
