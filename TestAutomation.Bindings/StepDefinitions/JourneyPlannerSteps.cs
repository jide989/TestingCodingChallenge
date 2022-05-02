using TechTalk.SpecFlow;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;
using TestAutomation.Bindings.Contexts;
using TestAutomation.PageObjects.Factories;

namespace TestAutomation.Bindings.StepDefinitions
{
    [Binding]
    public class JourneyPlannerSteps : BaseSteps
    {
        public ScenarioContext ScenarioContext { get; }

        public JourneyPlannerSteps(IPageObjectFactory pageObjectFactory, PageContext pageContext, ScenarioContext scenarioContext) : base(pageObjectFactory, pageContext)
        {
            ScenarioContext = scenarioContext;
        }

        [When(@"user changes the destination to London Waterloo")]
        public void WhenUserChangesTheDestinationToLondonWaterloo()
        {
            var fromStation = ScenarioContext.Get<string>("from");
            var toStation = "Waterloo (London), London Waterloo";
            ScenarioContext.Remove("to");
            ScenarioContext.Add("to", toStation);
            PageContext.JourneyResultPage = PageObjectFactory.CreateJourneyResultPage();
            PageContext.JourneyResultPage.UpdateJourney(fromStation, toStation);
        }

        [Then(@"user should be presented with the Journey Results page with the correct summary")]
        public void ThenUserShouldBePresentedWithTheJourneyResultsPageWithTheCorrectSummary()
        {
            var fromStation = ScenarioContext.Get<string>("from"); ;
            var toStation = ScenarioContext.Get<string>("to");
            PageContext.JourneyResultPage = PageObjectFactory.CreateJourneyResultPage();
            Assert.IsTrue(PageContext.JourneyResultPage.IsCorrectSummaryDisplayed(fromStation, toStation));
        }

        [Then(@"user can see the fastest route")]
        public void ThenUserCanSeeTheFastestRoute()
        {
            Assert.IsTrue(PageContext.JourneyResultPage.IsFastestRouteDisplayed());
        }

        [Given(@"user is on the TfL home page")]
        public void GivenUserIsOnTheTfLHomePage()
        {
            PageContext.TflHomePage = PageObjectFactory.CreateHomePage().Navigate();
            PageContext.TflHomePage.AcceptCookies();
        }

        [When(@"user enters text that does not match a station name into the journey planner")]
        public void WhenUserEntersTextThatDoesNotMatchAStationNameIntoTheJourneyPlanner()
        {
            var from = "1434324";
            var to = "1434324dsdfgs";
            PageContext.TflHomePage.PlanJourney(from, to);
        }

        [When(@"user clicks Plan my journey")]
        public void WhenUserClicksPlanMyJourney()
        {

        }

        [Then(@"user should be presented with the Journey Results page with an error message")]
        public void ThenUserShouldBePresentedWithTheJourneyResultsPageWithAnErrorMessage()
        {

            PageContext.JourneyResultPage = PageObjectFactory.CreateJourneyResultPage();
            Assert.IsTrue(PageContext.JourneyResultPage.IsErrorMessageDisplayed("Sorry, we can't find a journey matching your criteria"));
        }

        [When(@"user tries to plan a journey without a destination")]
        public void WhenUserTriesToPlanAJourneyWithoutADestination()
        {
            var from = "1434324";
            var to = " ";
            PageContext.TflHomePage.EnterFromAndTo(from, to);
            PageContext.TflHomePage.ClickPlanMyJourneyButton();

        }

        [Then(@"user sees an error message telling them that the To field is required")]
        public void ThenUserSeesAnErrorMessageTellingThemThatTheToFieldIsRequired()
        {
            Assert.IsTrue(PageContext.TflHomePage.IsErrorMessageDisplayed("The To field is required."));
        }


        [Given(@"user plans a journey from London Victoria to London Bridge")]
        [When(@"user plans a journey from London Victoria to London Bridge")]
        public void WhenUserPlansAJourneyFromLondonVictoriaToLondonBridge()
        {
            var from = "london victoria rail station";
            var to = "London Bridge, London Bridge Station";
            ScenarioContext.Add("from", from);
            ScenarioContext.Add("to", to);
            PageContext.TflHomePage.PlanJourney(from, to);
        }

    }
}
