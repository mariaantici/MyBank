using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.AutomatedTests.PageObjects
{
    class HomePage
    {
        private IWebDriver webDriver;

        public HomePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

        [FindsBy(How = How.LinkText, Using = "Login")]
        private IWebElement loginButton;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div[1]/div/div[1]/ul/li[1]/a")]
        private IWebElement ronSelector;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div[1]/div/div[2]/p")]
        private IWebElement amountOfSelectedCurrency;

        [FindsBy(How = How.XPath, Using = "/html/body/header/nav/div/div/ul[1]/li[3]/form/button")]
        private IWebElement logoutButton;

        public LoginPage GoToLoginPage()
        {
            loginButton.Click();
            return new LoginPage(webDriver);
        }

        public string GetRonAmount()
        {
            ronSelector.Click();
            return amountOfSelectedCurrency.Text;
        }

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44372/");
        }

        public void LogOut()
        {
            logoutButton.Click();
        }

    }
}