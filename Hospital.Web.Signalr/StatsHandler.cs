using Hospital.Events;
using NServiceBus;
using SignalR;
using log4net;

namespace Hospital.Web.Signalr
{
    public class StatsHandler :
        IHandleMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientDischarged>
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof (StatsHandler));

        private readonly IConnectionManager _connectionManager;

        public StatsHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        protected dynamic Clients
        {
            get { return _connectionManager.GetClients<StatsHub>(); }
        }


        public void Handle(PatientAdmitted message)
        {
            Log.DebugFormat("Admitted {0} {1}", message.FirstName, message.LastName);
            Clients.patientAdmitted();
        }

        public void Handle(BedAssigned message)
        {
            Log.DebugFormat("Assigned {0} to {1}", message.PatientId, message.Bed);
            Clients.bedAssigned();
        }

        public void Handle(PatientDischarged message)
        {
            Log.DebugFormat("Discharged {0}", message.PatientId);
            Clients.patientDischarged();
        }
    }
}
