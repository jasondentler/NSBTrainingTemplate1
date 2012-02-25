using System;
using System.Linq;

namespace Hospital.Commands
{
    public class CreatePatient : ICommand
    {

        public Guid CommandId { get; set; }
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("Patient Created: {0}",
                                 string.Join(", ", new[] {LastName, FirstName}
                                                       .Where(s => !string.IsNullOrWhiteSpace(s))
                                                       .Select(s => s.Trim())));
        }

    }
}
