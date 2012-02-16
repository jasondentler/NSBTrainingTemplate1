using System;
using System.Collections.Generic;

namespace Hospital
{
    public interface IHospitalReadService
    {
        HospitalStats GetHospitalStats();
        IEnumerable<PatientBedAssignment> GetAllAdmittedPatients();
        IEnumerable<PatientName> GetWaitingForBeds();
        PatientDetails GetPatientDetails(Guid patientId);
        IEnumerable<int> GetAvailableBeds();
    }
}
