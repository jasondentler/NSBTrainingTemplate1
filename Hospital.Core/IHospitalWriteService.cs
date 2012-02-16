using System;

namespace Hospital
{
    public interface IHospitalWriteService
    {

        void CreatePatient(Guid patientId, string firstName, string lastName);
        void AdmitPatient(Guid patientId, DateTimeOffset when);
        void AssignBed(Guid patientId, int bed);
        void DischargePatient(Guid patientId, DateTimeOffset when);

    }
}
