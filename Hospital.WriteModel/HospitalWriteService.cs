using System;
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
        }

        public void AdmitPatient(Guid patientId, DateTimeOffset when)
        {
        }

        public void AssignBed(Guid patientId, int bed)
        {
        }

        public void DischargePatient(Guid patientId, DateTimeOffset when)
        {
        }
    }
}
