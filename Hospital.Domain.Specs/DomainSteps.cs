using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Hospital.Domain.Specs
{
    [Binding]
    public class DomainSteps
    {

        private static DomainContext Context { get { return ScenarioContext.Current.Get<DomainContext>(); } }

        [BeforeScenario("domain")]
        public void BeforeDomain()
        {
            ScenarioContext.Current.Set(new DomainContext());
        }

        [Then(@"nothing happens"), Scope(Tag = "domain")]
        public void ThenNothingHappens()
        {
            Context.PublishedEvents.Should().Be.Empty();
            Context.Exception.Should().Be.Null();
        }

        [Then(@"nothing else happens"), Scope(Tag = "domain")]
        public void ThenNothingElseHappens()
        {
            Context.UncheckedEvents.Should().Be.Empty();
            Context.Exception.Should().Be.Null();
        }

        [Then(@"error: (.+)"), Scope(Tag = "domain")]
        public void ThenError(string message)
        {
            Context.Exception.Should().Not.Be.Null();
            Context.Exception.Message.Should().Be.EqualTo(message);
        }
    }
}
