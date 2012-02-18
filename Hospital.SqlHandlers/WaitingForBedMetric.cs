using System;
using Hospital.Events;
using NServiceBus;
using NServiceBus.Saga;

namespace Hospital.SqlHandlers
{
    public class WaitingForBedMetric : 
        Saga<WaitingForBedMetricState>,
        IAmStartedByMessages<PatientAdmitted>,
        IHandleMessages<BedAssigned>,
        IHandleMessages<PatientDischarged>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PatientAdmitted>(s => s.PatientId, e => e.PatientId);
            ConfigureMapping<BedAssigned>(s => s.PatientId, e => e.PatientId);
            ConfigureMapping<PatientDischarged>(s => s.PatientId, e => e.PatientId);
        }

        public void Handle(PatientAdmitted message)
        {
            Data.PatientId = message.PatientId;
            RequestUtcTimeout<object>(TimeSpan.FromHours(1));
        }

        public void Handle(BedAssigned message)
        {
            MarkAsComplete();
        }

        public void Handle(PatientDischarged message)
        {
            MarkAsComplete();
        }

        public override void Timeout(object state)
        {
            Bus.Send(new WaitingForBedMetricExceeded()
                          {
                              PatientId = Data.PatientId
                          });
        }
    }

    public class WaitingForBedMetricState : IContainSagaData
    {

        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public Guid PatientId { get; set; }

    }

    public class WaitingForBedMetricExceeded : Hospital.IMessage
    {

        public Guid PatientId { get; set; }

    }

}
