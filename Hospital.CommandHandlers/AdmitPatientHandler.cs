using Hospital.Commands;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AdmitPatientHandler : IHandleMessages<AdmitPatient>
    {
        public void Handle(AdmitPatient message)
        {
        }
    }
}
