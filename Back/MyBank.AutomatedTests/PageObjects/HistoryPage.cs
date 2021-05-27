using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.AutomatedTests.PageObjects
{
    class HistoryPage
    {
        private IWebDriver webDriver;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div/table/thead/tr[2]/td[2]")]
        private IWebElement lastTransactioUser;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div/table/thead/tr[2]/td[3]")]
        private IWebElement lastTransactionAmount;

        [FindsBy(How = How.XPath, Using = "/html/body/div/main/div/div/table/thead/tr[2]/td[4]")]
        private IWebElement lastTransactionType;

        public HistoryPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

        public void GoToPage()
        {
            webDriver.Navigate().GoToUrl("https://localhost:44372/History");
        }

        public List<string> GetLastTransaction()
        {
            List<string> lastTransaction = new List<string>();
            lastTransaction.Add(lastTransactioUser.Text);
            lastTransaction.Add(lastTransactionAmount.Text);
            lastTransaction.Add(lastTransactionType.Text);

            return lastTransaction;
        }
    }
}