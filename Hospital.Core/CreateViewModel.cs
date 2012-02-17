using System;

namespace Hospital
{
    public class CreateViewModel
    {
        public Guid CommandId { get; set; }
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CreateViewModel()
        {
        }

        public CreateViewModel(Guid commandId)
        {
            CommandId = commandId;
        }
    }
}
