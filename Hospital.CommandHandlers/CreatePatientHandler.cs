using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class CreatePatientHandler : 
        CommandHandler<Patient>, 
        IHandleMessages<CreatePatient>
    {

        public CreatePatientHandler(IRepository repository, IBus bus) : base(repository, bus)
        {
        }

        public void Handle(CreatePatient message)
        {
            var patient = new Patient(message.PatientId, message.FirstName, message.LastName);
            Save(patient, message);
        }

    }
}
