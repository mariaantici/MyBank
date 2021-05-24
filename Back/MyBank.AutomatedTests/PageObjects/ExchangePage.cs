using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.AutomatedTests.PageObjects
{
    public class ExchangePage
    {
        [FindsBy(How = How.Id, Using = "amount")]
        private IWebElement amount;

        [FindsBy(How = How.Id, Using = "agreements")]
        private IWebElement checkbox;

        [FindsBy(How = How.Id, Using = "SubmitBtn")]
        private IWebElement submitButton;

        private IWebDriver webDriver;
        public ExchangePage(IWebDriver driver)
        {
            webDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44372/Transactions/Exchange");
        }

        public void Submit(string insertedAmount)
        {
            amount.Clear();

            amount.SendKeys(insertedAmount);

            checkbox.Click();

            submitButton.Click();
        }
    }
}
