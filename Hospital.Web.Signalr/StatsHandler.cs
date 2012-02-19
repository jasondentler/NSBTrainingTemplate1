using Hospital.Events;
using NServiceBus;
using SignalR;
using SignalR.Hubs;
using log4net;

namespace Hospital.Web.Signalr
{

    public class StatsHub : Hub
    {
    }

    public abstract class StatsHandler
    {
        private readonly IConnectionManager _connectionManager;

        protected StatsHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        protected dynamic Clients
        {
            get
            {
                return _connectionManager.GetClients<StatsHub>();
            }
        }
    }

    public class PatientAdmittedStatsHandler : 
        StatsHandler,
        IHandleMessages<PatientAdmitted>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientAdmittedStatsHandler));

        public PatientAdmittedStatsHandler(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public void Handle(PatientAdmitted message)
        {
            Log.DebugFormat("Admitted {0} {1}", message.FirstName, message.LastName);
            Clients.patientAdmitted();
        }

    }

    public class BedAssignedStatsHandler : 
        StatsHandler,
        IHandleMessages<BedAssigned>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BedAssignedStatsHandler));

        public BedAssignedStatsHandler(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public void Handle(BedAssigned message)
        {
            Log.DebugFormat("Assigned {0} to {1}", message.PatientId, message.Bed);
            Clients.bedAssigned();
        }

    }

    public class PatientDischargedStatsHandler : 
        StatsHandler,
        IHandleMessages<PatientDischarged>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PatientDischargedStatsHandler));

        public PatientDischargedStatsHandler(IConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public void Handle(PatientDischarged message)
        {
            Log.DebugFormat("Discharged {0}", message.PatientId);
            Clients.patientDischarged();
        }
    }

}
