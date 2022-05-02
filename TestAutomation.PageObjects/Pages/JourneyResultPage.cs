using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using TestAutomation.Framework.BasePages;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.PageObjects.Pages
{
    // This page and all other page classes should inherit from BasePage<T>
    public class JourneyResultPage : BasePage<JourneyResultPage>
    {
        internal JourneyResultPage(IWebDriverManager webDriverManager) : base(webDriverManager)
        {
        }

        private IWebElement FromStationLabel => WebDriver.FindElement(By.XPath("//span[text() = 'From:']/../span/strong"));   
        private IWebElement ToStationLabel => WebDriver.FindElement(By.XPath("//span[text() = 'To:']/../span/strong"));
        private IWebElement FastestRouteSection => WebDriver.FindElement(By.CssSelector(".jp-result-transport"));
        private IWebElement EditJourneyLink => WebDriver.FindElement(By.CssSelector(".edit-journey"));
        private IWebElement UpdateJourneyButton => WebDriver.FindElement(By.Id("plan-journey-button"));
        private IWebElement FromInput => WebDriver.FindElement(By.Id("InputFrom"));
        private IWebElement FromInputClearButton => WebDriver.FindElement(By.CssSelector("#search-filter-form-0 .remove-content"));
        private IWebElement ToInput => WebDriver.FindElement(By.Id("InputTo"));
        private IWebElement ToInputClearButton => WebDriver.FindElement(By.CssSelector("#search-filter-form-1 .remove-content"));
        private IWebElement ErrorMessage => WebDriver.FindElement(By.CssSelector(".results-wrapper .field-validation-error"));
        public bool PageLoaded => FromStationLabel.Displayed;

 
        public override JourneyResultPage WaitForPageElements()
        {
            WebDriverWait.Until(driver => PageLoaded);
            return this;
        }

        public bool IsCorrectSummaryDisplayed(string fromStation, string toStation)
        {  
            return FromStationLabel.Text.Contains(fromStation) & ToStationLabel.Text.Contains(toStation);
        }

        public bool IsErrorMessageDisplayed(string errorMessage)
        {
            WebDriverWait.Until(driver => ErrorMessage.Displayed);
            return ErrorMessage.Text.Contains(errorMessage);
        }

        public bool? IsFastestRouteDisplayed()
        {
            WebDriverWait.Until(driver => FastestRouteSection.Displayed);
            return FastestRouteSection.Text.Contains("Fastest by public transport");
        }

        public void UpdateJourney(string from, string to)
        {
            EditJourneyLink.Click();
            WebDriverWait.Until(driver => FromInputClearButton.Displayed);
            FromInputClearButton.Click();
            FromInput.SendKeys(from);
            new Actions(WebDriver).SendKeys(Keys.Tab).Perform();
            ToInputClearButton.Click();
            ToInput.SendKeys(to);
            UpdateJourneyButton.Click();
        }
    }
}