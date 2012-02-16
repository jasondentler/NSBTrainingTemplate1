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

        [Given(@"I have created a patient")]
        public void GivenIHaveCreatedAPatient(Table table)
        {
            var e = new PatientCreated()
                          {
                              EventId = Guid.NewGuid(),
                              PatientId = Guid.NewGuid(),
                              FirstName = "Bob",
                              LastName = "Smith"
                          };

            table.FillInstance(e);
            
            PatientId = e.PatientId;

            Context.Given(e.PatientId, e);
        }

        [Given(@"I have admitted the patient")]
        public void GivenIHaveAdmittedThePatient()
        {

            ScenarioContext.Current.Pending();
        }

        [Given(@"I have assigned the patient to a bed")]
        public void GivenIHaveAssignedThePatientToABed(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I admit the patient")]
        public void WhenIAdmitThePatient()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I assign the patient to a bed")]
        public void WhenIAssignThePatientToABed(Table table)
        {
            ScenarioContext.Current.Pending();
        }

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

        [When(@"I discharge the patient")]
        public void WhenIDischargeThePatient()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the patient is admitted")]
        public void ThenThePatientIsAdmitted()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the patient is assigned to a bed")]
        public void ThenThePatientIsAssignedToABed(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the patient is created")]
        public void ThenThePatientIsCreated(Table table)
        {
            var e = Context.Then<PatientCreated>();
            
            table.CompareToInstance(e);

            e.PatientId.Should().Be.EqualTo(PatientId);
        }

        [Then(@"the patient is discharged")]
        public void ThenThePatientIsDischarged()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the patient is moved")]
        public void ThenThePatientIsMoved(Table table)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
