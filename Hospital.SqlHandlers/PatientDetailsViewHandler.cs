using System;
using Hospital.Events;
using NServiceBus;
using Simple.Data;

namespace Hospital.SqlHandlers
{
    public class PatientDetailsViewHandler :
        IHandleMessages<PatientCreated>,
        IHandleMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientMoved>,
        IHandleMessages<PatientDischarged>
    {
        public void Handle(PatientCreated message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.PatientDetails.Insert(new
                                         {
                                             message.PatientId,
                                             message.FirstName,
                                             message.LastName,
                                             BedAssignment = (int?) null,
                                             Admitted = (DateTime?) null,
                                             Discharged = (DateTime?) null,
                                             CanAdmit = true,
                                             CanAssignBed = false,
                                             CanDischarge = false
                                         });
        }

        public void Handle(PatientAdmitted message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.PatientDetails.UpdateByPatientId(new
                                                    {
                                                        message.PatientId,
                                                        Admitted = message.When,
                                                        CanAdmit = false,
                                                        CanAssignBed = true,
                                                        CanDischarge = true
                                                    });
        }

        public void Handle(BedAssigned message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.PatientDetails.UpdateByPatientId(new
                                              {
                                                  message.PatientId,
                                                  BedAssignment = message.Bed
                                              });
        }

        public void Handle(PatientMoved message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.PatientDetails.UpdateByPatientId(new
                                              {
                                                  message.PatientId,
                                                  BedAssignment = message.ToBed
                                              });
        }

        public void Handle(PatientDischarged message)
        {
            var db = Database.OpenNamedConnection(Constants.ConnectionStringName);
            db.PatientDetails.UpdateByPatientId(new
                                                    {
                                                        message.PatientId,
                                                        BedAssignment = (int?) null,
                                                        Discharged = message.When,
                                                        CanAssignBed = true,
                                                        CanDischarge = false
                                                    });
        }
    }
}
