using System;
using Hospital.Commands;
using NServiceBus;

namespace Hospital.WriteModel
{
    public class HospitalWriteService : IHospitalWriteService
    {
        private readonly IBus _bus;

        public HospitalWriteService(IBus bus)
        {
            _bus = bus;
        }

        public void CreatePatient(Guid patientId, string firstName, string lastName)
        {
            var cmd = new CreatePatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = patientId,
                              FirstName = firstName,
                              LastName = lastName
                          };
            _bus.Send(cmd);
        }

        public void AdmitPatient(Guid patientId, DateTimeOffset when)
        {
            var cmd = new AdmitPatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = patientId,
                              When = when
                          };
            _bus.Send(cmd);
        }

        public void AssignBed(Guid patientId, int bed)
        {
            var cmd = new AssignBed()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = patientId,
                              Bed = bed
                          };
            _bus.Send(cmd);
        }

        public void DischargePatient(Guid patientId, DateTimeOffset when)
        {
            var cmd = new DischargePatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = patientId,
                              When = when
                          };
            _bus.Send(cmd);
        }
    }
}
