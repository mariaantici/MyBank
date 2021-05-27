using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBank.AutomatedTests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace MyBank.AutomatedTests
{
    [TestClass]
    public class ExchangePageTest
    {
        IWebDriver driver;


        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
        }


        [TestMethod]
        public void Exchange_Creates_GivenValidAmount()
        {

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("radu@gmail.com", "8GTD5CKn`LGay[qD");

            ExchangePage exchangePage = new ExchangePage(driver);

            exchangePage.GoToPage();

            exchangePage.Submit("1");

            var urlAfterExchange = driver.Url;

            HistoryPage historyPage = new HistoryPage(driver);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var list = historyPage.GetLastTransaction();

            Assert.AreEqual(urlAfterExchange, "https://localhost:44372/History");
            Assert.IsTrue(list[0] == "radu@gmail.com");
            Assert.IsTrue(list[1] == "1");
            Assert.IsTrue(list[2] == "RON->EUR");
        }

        [TestMethod]
        public void Exchange_Creates_GivenInvalidAmount()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("radu@gmail.com", "8GTD5CKn`LGay[qD");

            ExchangePage exchangePage = new ExchangePage(driver);

            exchangePage.GoToPage();

            exchangePage.Submit("999999");

            var urlAfterExchange = driver.Url;

            HistoryPage historyPage = new HistoryPage(driver);

            historyPage.GoToPage();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var list = historyPage.GetLastTransaction();


            Assert.AreEqual(urlAfterExchange, "https://localhost:44372/Transactions/Failure?errorType=balance");
            //Assert.AreNotEqual(list[0], "radu@gmail.com");
            //Assert.AreNotEqual(list[1],  "1");
            //Assert.AreNotEqual(list[2], "RON->EUR");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}