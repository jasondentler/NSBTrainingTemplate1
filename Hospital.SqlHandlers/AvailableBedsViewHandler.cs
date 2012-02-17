using Hospital.Events;
using NServiceBus;
using Simple.Data;

namespace Hospital.SqlHandlers
{
    public class AvailableBedsViewHandler :
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientMoved>,
        IHandleMessages<PatientDischarged>
    {

        public void Handle(BedAssigned message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.AvailableBeds.DeleteByBed(message.Bed);
        }

        public void Handle(PatientMoved message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            using (var tx = db.BeginTransaction())
            {
                tx.AvailableBeds.Insert(Bed: message.FromBed);
                tx.AvailableBeds.DeleteByBed(message.ToBed);
                tx.Commit();
            }
        }

        public void Handle(PatientDischarged message)
        {
            if (!message.Bed.HasValue) return;
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.AvailableBeds.Insert(Bed: message.Bed);
        }

    }
}
