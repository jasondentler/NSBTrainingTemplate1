using System;
using CommonDomain.Persistence;
using Hospital.Commands;
using Hospital.Domain;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class CreatePatientHandler : IHandleMessages<CreatePatient>
    {
        private readonly IRepository _repository;

        public CreatePatientHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreatePatient message)
        {
            var patient = new Patient(message.PatientId, message.FirstName, message.LastName);
            _repository.Save(patient, Guid.NewGuid());
        }

    }
}
