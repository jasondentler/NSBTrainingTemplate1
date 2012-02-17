using System;
using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class DischargePatientHandler : IHandleMessages<DischargePatient>
    {
        private readonly IRepository _repository;

        public DischargePatientHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(DischargePatient message)
        {
            var patient = _repository.GetById<Patient>(message.PatientId);
            patient.Discharge();
            _repository.Save(patient, Guid.NewGuid());
        }
    }
}
