using Hospital.Commands;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class DischargePatientHandler : IHandleMessages<DischargePatient>
    {
        public void Handle(DischargePatient message)
        {
        }
    }
}
