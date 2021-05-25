using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverTests.PageObjects
{
    class TransferPage
    {
        private IWebDriver webDriver;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/form/div[1]/input")]
        private IWebElement userNameField;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/form/div[2]/input")]
        private IWebElement amountField;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/form/div[3]/select")]
        private IWebElement typeSelect;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/form/button")]
        private IWebElement sendButton;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/form/div[4]/input")]
        private IWebElement checkButton;

        public TransferPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44372/Transactions/Transfer");
        }

        public void TransferRON(string userName, float amount)
        {
            userNameField.Clear();
            userNameField.SendKeys(userName);
            amountField.Clear();
            amountField.SendKeys(amount.ToString());
            var selectElement = new SelectElement(typeSelect);
            selectElement.SelectByText("RON");
            checkButton.Click();
            sendButton.Click();

        }
    }
}
