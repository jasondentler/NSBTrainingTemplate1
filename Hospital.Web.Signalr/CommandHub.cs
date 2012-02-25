using System;
using Hospital.Commands;
using Microsoft.Practices.ServiceLocation;
using NServiceBus;
using SignalR.Hubs;
using log4net;

namespace Hospital.Web.Signalr
{
    public class CommandHub : Hub
    {

        public static readonly ILog Log = LogManager.GetLogger(typeof(CommandHub));

        public const string ClientIdKey = "SignalR.ClientId";

        private readonly IBus _bus;

        public CommandHub()
            : this(GetBus())
        {
        }

        private static IBus GetBus()
        {
            return ServiceLocator.Current.GetInstance<IBus>();
        }

        public CommandHub(IBus bus)
        {
            _bus = bus;
        }

        private T Send<T>(T command) where T : class, ICommand
        {
            if (command == null) throw new ArgumentNullException("command");
            command.SetHeader(ClientIdKey, Context.ConnectionId);
            _bus.Send(command);
            Log.DebugFormat("{0} Command Sent. {1}", typeof(T).Name, command.ToString());
            Caller.commandSent(typeof(T).Name, command.ToString(), command.CommandId);
            return command;
        }

        public CreatePatient CreatePatient(string patientId, string firstName, string lastName)
        {
            var cmd = new CreatePatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = Guid.Parse(patientId),
                              FirstName = firstName,
                              LastName = lastName
                          };
            return Send(cmd);
        }

        public AdmitPatient AdmitPatient(string patientId)
        {
            var cmd = new AdmitPatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = Guid.Parse(patientId),
                              When = DateTimeOffset.UtcNow
                          };
            return Send(cmd);
        }

        public AssignBed AssignBed(string patientId, int bed)
        {
            var cmd = new AssignBed()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = Guid.Parse(patientId),
                              Bed = bed
                          };
            return Send(cmd);
        }

        public DischargePatient DischargePatient(string patientId)
        {
            var cmd = new DischargePatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = Guid.Parse(patientId),
                              When = DateTimeOffset.UtcNow
                          };
            return Send(cmd);
        }


    }
}
