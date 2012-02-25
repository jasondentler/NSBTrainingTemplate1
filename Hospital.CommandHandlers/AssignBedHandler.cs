using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AssignBedHandler : 
        CommandHandler<Patient>,
        IHandleMessages<AssignBed>
    {
        public AssignBedHandler(IRepository repository, IBus bus) : base(repository,bus)
        {
        }

        public void Handle(AssignBed message)
        {
            var patient = Get(message.PatientId);
            patient.AssignBed(message.Bed);
            Save(patient, message);
        }
    }
}
