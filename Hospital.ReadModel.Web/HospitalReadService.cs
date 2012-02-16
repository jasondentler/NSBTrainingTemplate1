using System;
using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using Simple.Data.RawSql;

namespace Hospital.ReadModel.Web
{
    public class HospitalReadService : IHospitalReadService
    {

        private const string ConnectionName = "Hospital.ReadModel.Web";

        public HospitalStats GetHospitalStats()
        {
            Database db = Database.OpenNamedConnection(ConnectionName);
            var row = db.ToRow("SELECT TOP 1 AdmittedPatients, AvailableBeds, WaitingForBeds FROM Stats");
            return new HospitalStats()
                       {
                           AdmittedPatients = row.AdmittedPatients,
                           AvailableBeds = row.AvailableBeds,
                           WaitingForBeds = row.WaitingForBeds
                       };
        }

        public IEnumerable<PatientBedAssignment> GetAllAdmittedPatients()
        {
            Database db = Database.OpenNamedConnection(ConnectionName);
            var rows = db.ToRows("SELECT PatientId, FirstName, LastName, BedAssignment FROM AdmittedPatients ORDER BY LastName, FirstName");
            return rows.Select(r => new PatientBedAssignment()
                                        {
                                            PatientId = r.PatientId,
                                            FirstName = r.FirstName,
                                            LastName = r.LastName,
                                            BedAssignment = r.BedAssignment
                                        });
        }

        public IEnumerable<PatientName> GetWaitingForBeds()
        {
            Database db = Database.OpenNamedConnection(ConnectionName);
            var rows = db.ToRows("SELECT PatientId, FirstName, LastName FROM WaitingForBeds ORDER BY LastName, FirstName");
            return rows.Select(r => new PatientName()
                                        {
                                            PatientId = r.PatientId,
                                            FirstName = r.FirstName,
                                            LastName = r.LastName
                                        });
        }

        public PatientDetails GetPatientDetails(Guid patientId)
        {
            Database db = Database.OpenNamedConnection(ConnectionName);
            var row =
                db.ToRow(
                    @"SELECT TOP 1
                        PatientId, FirstName, LastName, BedAssignment, Admitted, Discharged, 
                        CanAdmit, CanAssignBed, CanDischarge
                    FROM PatientDetails
                    WHERE PatientId = @patientId",
                    new {patientId});

            return row == null
                       ? null
                       : new PatientDetails()
                             {
                                 PatientId = row.PatientId,
                                 FirstName = row.FirstName,
                                 LastName = row.LastName,
                                 BedAssignment = row.BedAssignment,
                                 Admitted = row.Admitted,
                                 Discharged = row.Discharged,
                                 CanAdmit = row.CanAdmit,
                                 CanAssignBed = row.CanAssignBed,
                                 CanDischarge = row.CanDischarge
                             };
        }

        public IEnumerable<int> GetAvailableBeds()
        {
            Database db = Database.OpenNamedConnection(ConnectionName);
            var rows = db.ToRows("SELECT Bed FROM AvailableBeds ORDER BY Bed ASC");
            return rows.Select(r => (int) r.Bed);
        }
    }
}
