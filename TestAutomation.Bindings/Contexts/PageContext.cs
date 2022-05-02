using TestAutomation.PageObjects.Pages;

namespace TestAutomation.Bindings.Contexts
{
    public class PageContext
    {
        public LoginPage LoginPage { get; set; }
        public TflHomePage TflHomePage { get; set; }
        public SecureAreaPage SecureAreaPage { get; set; }
        public JourneyResultPage JourneyResultPage { get; set; }
    }
}
