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
            Execute("UPDATE Stats SET AdmittedPatients = AdmittedPatients + 1, WaitingForBeds = WaitingForBeds + 1");
        }

        public void Handle(BedAssigned message)
        {
            Execute("UPDATE Stats SET AvailableBeds = AvailableBeds - 1, WaitingForBeds = WaitingForBeds - 1");
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
            Execute("UPDATE Stats SET AdmittedPatients = AdmittedPatients - 1, AvailableBeds = AvailableBeds + 1");
        }

        private void HandlePatientDischargedWithoutBed()
        {
            Execute("UPDATE Stats SET AdmittedPatients = AdmittedPatients - 1, WaitingForBeds = WaitingForBeds - 1");
        }

        private void Execute(string sql)
        {
            var connStr = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString;
            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}
