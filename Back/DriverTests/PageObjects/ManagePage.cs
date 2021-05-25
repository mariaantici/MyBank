using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverTests.PageObjects
{
    class ManagePage
    {

        private IWebDriver webDriver;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div/div[2]/div[1]/div/form/div[2]/input")]
        private IWebElement phoneNumberField;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div/div[2]/div[1]/div/form/button")]
        private IWebElement savePhoneNumberButton;

        public ManagePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44372/Identity/Account/Manage");
        }

        public void SavePhoneNumber(string phoneNumber)
        {
            phoneNumberField.Clear();
            phoneNumberField.SendKeys(phoneNumber);
            savePhoneNumberButton.Click();
        }

        public string GetPhoneNumber()
        {
            return phoneNumberField.GetAttribute("value");
        }

    }
}
