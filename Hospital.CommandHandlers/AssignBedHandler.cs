using System;
using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AssignBedHandler : IHandleMessages<AssignBed>
    {
        private readonly IRepository _repository;

        public AssignBedHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AssignBed message)
        {
            var patient = _repository.GetById<Patient>(message.PatientId);
            patient.AssignBed(message.Bed);
            _repository.Save(patient, Guid.NewGuid());
        }
    }
}
