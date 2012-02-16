using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hospital.Commands;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AdmitPatientHandler : IHandleMessages<AdmitPatient>
    {
        public void Handle(AdmitPatient message)
        {
            throw new NotImplementedException();
        }
    }
}
