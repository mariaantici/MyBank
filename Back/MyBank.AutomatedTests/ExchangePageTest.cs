using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBank.AutomatedTests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyBank.Automation
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

            var url = driver.Url;

            Assert.AreEqual(url, "https://localhost:44372/History");
        }

        [TestMethod]
        public void Exchange_Creates_GivenInvalidAmount()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("radu@gmail.com", "8GTD5CKn`LGay[qD");

            ExchangePage exchangePage = new ExchangePage(driver);

            exchangePage.GoToPage();

            exchangePage.Submit("999999");

            var url = driver.Url;

            Assert.AreEqual(url, "https://localhost:44372/Transactions/Failure?errorType=balance");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}