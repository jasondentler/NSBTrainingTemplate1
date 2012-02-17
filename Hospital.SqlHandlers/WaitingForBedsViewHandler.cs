using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hospital.Events;
using NServiceBus;
using Simple.Data;

namespace Hospital.SqlHandlers
{
    public class WaitingForBedsViewHandler :
        IHandleMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientDischarged>
    {

        public void Handle(PatientAdmitted message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.WaitingForBeds.Insert(new {message.PatientId, message.FirstName, message.LastName});
        }

        public void Handle(BedAssigned message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.WaitingForBeds.DeletedByPatientId(message.PatientId);
        }

        public void Handle(PatientDischarged message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.WaitingForBeds.DeletedByPatientId(message.PatientId);
        }

    }
}
