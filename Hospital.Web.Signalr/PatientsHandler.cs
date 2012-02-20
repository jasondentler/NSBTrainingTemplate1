using Hospital.Events;
using NServiceBus;
using SignalR;
using SignalR.Hubs;
using log4net;

namespace Hospital.Web.Signalr
{

    public class PatientsHub : Hub
    {
    }

    public class PatientsHandler :
        IHandleMessages<PatientCreated>,
        IHandleMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientMoved>,
        IHandleMessages<PatientDischarged>
    {

        public static readonly ILog Log = LogManager.GetLogger(typeof (PatientsHandler));

        private readonly IConnectionManager _connectionManager;

        public PatientsHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public dynamic Clients
        {
            get { return _connectionManager.GetClients<PatientsHub>(); }
        }


        public void Handle(PatientCreated message)
        {
            Log.DebugFormat("Created {0} {1}", message.FirstName, message.LastName);
            Clients.patientCreated(message);
        }


        public void Handle(PatientAdmitted message)
        {
            Log.DebugFormat("Admitted {0}", message.PatientId);
            Clients.patientAdmitted(message);
        }

        public void Handle(BedAssigned message)
        {
            Log.DebugFormat("Assigned {0} to bed {1}", message.PatientId, message.Bed);
            Clients.bedAssigned(message);
        }

        public void Handle(PatientMoved message)
        {
            Log.DebugFormat("Moved {0} from bed {1} to bed {2}", message.PatientId, message.FromBed, message.ToBed);
            Clients.patientMoved(message);
        }

        public void Handle(PatientDischarged message)
        {
            Log.DebugFormat("Discharged {0}", message.PatientId);
            Clients.patientDischarged(message);
        }
    }
}

