using System;

namespace Hospital
{
    public class PatientName
    {

        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string LastFirst { get { return string.Join(", ", new[] { LastName, FirstName }); } }
    }
}
