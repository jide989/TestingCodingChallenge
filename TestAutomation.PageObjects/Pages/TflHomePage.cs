using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using TestAutomation.Framework.BasePages;
using TestAutomation.Framework.Interfaces;

namespace TestAutomation.PageObjects.Pages
{
    // This page and all other page classes should inherit from BasePage<T>
    public class TflHomePage : BasePage<TflHomePage>
    {
        internal TflHomePage(IWebDriverManager webDriverManager) : base(webDriverManager)
        {
        }

        private IWebElement FromInput => WebDriver.FindElement(By.Id("InputFrom"));        
        private IWebElement ToInput => WebDriver.FindElement(By.Id("InputTo"));        
        private IWebElement PlanJourneyButton => WebDriver.FindElement(By.Id("plan-journey-button"));        
        private IWebElement ErrorMessage => WebDriver.FindElement(By.CssSelector("#InputTo-error"));        
        private IWebElement AcceptAllCookiesButton => WebDriver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));        
        private IWebElement CookiesDoneButton => WebDriver.FindElement(By.CssSelector("#cb-confirmedSettings .cb-button"));        

        public bool PageLoaded => FromInput.Displayed;

        public TflHomePage Navigate()
        {
            WebDriverManager.WebDriver.Navigate().GoToUrl(WebDriverManager.RootUrl);
            return this;
        }

        public override TflHomePage WaitForPageElements()
        {
            WebDriverWait.Until(driver => PageLoaded);
            return this;
        }

        public void PlanJourney(string from, string to)
        {
            EnterFromAndTo(from, to);
            PlanJourneyButton.Click();
        }

        public void EnterFromAndTo(string from, string to)
        {
            FromInput.SendKeys(from);
            new Actions(WebDriver).SendKeys(Keys.Tab).Perform();
            ToInput.SendKeys(to);
            new Actions(WebDriver).SendKeys(Keys.Tab).Perform();
        }

        public void ClickPlanMyJourneyButton()
        {
            WebDriverWait.Until(driver => PlanJourneyButton.Displayed);
            PlanJourneyButton.Click();
        }

        public bool? IsErrorMessageDisplayed(string errorMessage)
        {
            WebDriverWait.Until(driver => ErrorMessage.Displayed);
            return ErrorMessage.Text.Contains(errorMessage);
        }

        public TflHomePage AcceptCookies()
        {
            WebDriverWait.Until(driver => AcceptAllCookiesButton.Displayed);
            AcceptAllCookiesButton.Click();
            WebDriverWait.Until(driver => CookiesDoneButton.Displayed);
            CookiesDoneButton.Click();
            return this;
        }
    }
}