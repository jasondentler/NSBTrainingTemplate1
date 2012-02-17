using System.Configuration;
using System.Data.SqlClient;
using Hospital.Events;
using NServiceBus;
using Simple.Data;
using Simple.Data.RawSql;

namespace Hospital.SqlHandlers
{
    public class StatsViewHandler :
        IHandleMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientDischarged>
    {

        public void Handle(PatientAdmitted message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Stats.UpdateAll(
                AdmittedPatients: db.Stats.AdmittedPatients + 1,
                WaitingForBeds: db.Stats.WaitingForBeds + 1);
        }

        public void Handle(BedAssigned message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Stats.UpdateAll(
                AvailableBeds: db.Stats.AvailableBeds + 1,
                WaitingForBeds: db.Stats.WaitingForBeds - 1);
        }

        public void Handle(PatientDischarged message)
        {
            if (message.Bed.HasValue)
            {
                HandlePatientDischargedWithBed();
            }
            else
            {
                HandlePatientDischargedWithoutBed();
            }
        }

        private void HandlePatientDischargedWithBed()
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Stats.UpdateAll(
                AdmittedPatients: db.Stats.AdmittedPatients - 1,
                AvailableBeds: db.Stats.WaitingForBeds + 1);
        }

        private void HandlePatientDischargedWithoutBed()
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Stats.UpdateAll(
                AdmittedPatients: db.Stats.AdmittedPatients - 1,
                WaitingForBeds: db.Stats.WaitingForBeds - 1);
        }

    }
}
