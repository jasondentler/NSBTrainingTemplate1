using System;
using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AdmitPatientHandler : IHandleMessages<AdmitPatient>
    {
        private readonly IRepository _repository;

        public AdmitPatientHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AdmitPatient message)
        {
            var patient = _repository.GetById<Patient>(message.PatientId);
            patient.Admit(message.When);
            _repository.Save(patient, Guid.NewGuid());
        }
    }
}
