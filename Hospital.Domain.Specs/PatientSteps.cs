using TechTalk.SpecFlow;

namespace Hospital.Domain.Specs
{
    [Binding]
    public class PatientSteps
    {
        [When(@"I create a patient")]
        public void WhenICreateAPatient(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the patient is created")]
        public void ThenThePatientIsCreated(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the patient is moved")]
        public void ThenThePatientIsMoved(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"nothing else happens")]
        public void ThenNothingElseHappens()
        {
            ScenarioContext.Current.Pending();
        }


    }
}
