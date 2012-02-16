using System;
using Hospital.Commands;
using Hospital.Events;
using SharpTestsEx;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Hospital.Domain.Specs
{
    [Binding]
    public class PatientSteps
    {

        private static DomainContext Context { get { return ScenarioContext.Current.Get<DomainContext>(); } }

        public Guid PatientId;

        [When(@"I create a patient")]
        public void WhenICreateAPatient(Table table)
        {
            var cmd = new CreatePatient()
                          {
                              CommandId = Guid.NewGuid(),
                              PatientId = Guid.NewGuid(),
                              FirstName = "Bob",
                              LastName = "Smith"
                          };

            table.FillInstance(cmd);

            PatientId = cmd.PatientId;

            Context.When(cmd);
        }

        [Then(@"the patient is created")]
        public void ThenThePatientIsCreated(Table table)
        {
            var e = Context.Then<PatientCreated>();
            
            table.CompareToInstance(e);

            e.PatientId.Should().Be.EqualTo(PatientId);
        }

    }
}
