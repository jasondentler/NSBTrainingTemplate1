using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class DischargePatientHandler : 
        CommandHandler<Patient>,
        IHandleMessages<DischargePatient>
    {

        public DischargePatientHandler(IRepository repository, IBus bus) : base(repository, bus)
        {
        }

        public void Handle(DischargePatient message)
        {
            var patient = Get(message.PatientId);
            patient.Discharge();
            Save(patient, message);
        }
    }
}
