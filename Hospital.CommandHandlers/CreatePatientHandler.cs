using System;
using Hospital.Commands;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class CreatePatientHandler : IHandleMessages<CreatePatient>
    {
        public void Handle(CreatePatient message)
        {
            Console.WriteLine("Creat patient {0}", message.PatientId);
        }
    }
}
