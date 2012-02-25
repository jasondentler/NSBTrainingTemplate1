using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AdmitPatientHandler : 
        CommandHandler<Patient>,
        IHandleMessages<AdmitPatient>
    {
        
        public AdmitPatientHandler(IRepository repository, IBus bus) : base(repository,bus)
        {
        }

        public void Handle(AdmitPatient message)
        {
            var patient = Get(message.PatientId);
            patient.Admit(message.When);
            Save(patient, message);
        }
    }
}
