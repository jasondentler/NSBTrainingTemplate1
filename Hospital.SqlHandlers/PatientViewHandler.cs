using Hospital.Events;
using NServiceBus;
using Simple.Data;

namespace Hospital.SqlHandlers
{
    public class PatientViewHandler :
        IHandleMessages<PatientCreated>,
        IHandleMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientMoved>,
        IHandleMessages<PatientDischarged>
    {
        public void Handle(PatientCreated message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Patients.Insert(new
                                   {
                                       message.PatientId,
                                       message.FirstName,
                                       message.LastName,
                                       BedAssignment = (int?) null,
                                       IsAdmitted = false,
                                       IsDischarged = false
                                   });
        }

        public void Handle(PatientAdmitted message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Patients.UpdateByPatientId(new
                                              {
                                                  message.PatientId,
                                                  IsAdmitted = true
                                              });
        }

        public void Handle(BedAssigned message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Patients.UpdateByPatientId(new
                                              {
                                                  message.PatientId,
                                                  BedAssignment = message.Bed
                                              });
        }

        public void Handle(PatientMoved message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Patients.UpdateByPatientId(new
                                              {
                                                  message.PatientId,
                                                  BedAssignment = message.ToBed
                                              });
        }

        public void Handle(PatientDischarged message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.Patients.UpdateByPatientId(new
                                              {
                                                  message.PatientId,
                                                  BedAssignment = (int?) null,
                                                  IsDischarged = true
                                              });
        }
    }
}
