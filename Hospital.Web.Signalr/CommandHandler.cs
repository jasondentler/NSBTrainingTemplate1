using Hospital.Events;
using NServiceBus;
using SignalR;
using log4net;

namespace Hospital.Web.Signalr
{
    public class CommandHandler :
        IHandleMessages<CommandProcessed>
    {

        public static readonly ILog Log = LogManager.GetLogger(typeof(CommandHandler));

        private readonly IConnectionManager _connectionManager;

        public CommandHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        private dynamic Clients
        {
            get { return _connectionManager.GetClients<CommandHub>(); }
        }

        public void Handle(CommandProcessed message)
        {
            Log.DebugFormat("Command {0} processed", message.CommandId);
            var clientId = message.GetHeader(CommandHub.ClientIdKey);
            Clients[clientId].commandProcessed(message.CommandId);
        }
    }
}
