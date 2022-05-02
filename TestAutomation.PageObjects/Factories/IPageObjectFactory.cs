using TestAutomation.PageObjects.Pages;

namespace TestAutomation.PageObjects.Factories
{
    public interface IPageObjectFactory
    {
        LoginPage CreateLoginPage();
        TflHomePage CreateHomePage();
        JourneyResultPage CreateJourneyResultPage();
    }
}