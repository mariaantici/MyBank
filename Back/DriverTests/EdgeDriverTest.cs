using CourseManager.AutomatedTests.PageObjects;
using DriverTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace DriverTests
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private  IWebDriver webDriver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        { 
            webDriver = new ChromeDriver(@"C:\Users\VladC\Desktop");
        }
        [TestMethod]
        public void VerifyRONSendAmount()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("vlad123@gmail.com", "vlad123!A");
            IAlert alertf = webDriver.SwitchTo().Alert();
            alertf.Accept();

            var initialValue = homePage.GetRonAmount();
            var value = initialValue.Split(" ");
            var initialValue1 = float.Parse(value[0]);

            TransferPage transferPage = new TransferPage(webDriver);
            transferPage.GoToPage();
            transferPage.TransferRON("vlad12345@gmail.com", 1);
            homePage.GoToPage();

            var afterTransferValue = homePage.GetRonAmount();
            value = afterTransferValue.Split(" ");
            var afterTransferValue1 = float.Parse(value[0]);

            Assert.IsTrue(initialValue1 > afterTransferValue1);
        }

        [TestMethod]
        public void VerifyHistoryEntryAfterTransfer()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("vlad123@gmail.com", "vlad123!A");
            IAlert alertf = webDriver.SwitchTo().Alert();
            alertf.Accept();

            TransferPage transferPage = new TransferPage(webDriver);
            transferPage.GoToPage();
            transferPage.TransferRON("vlad12345@gmail.com", 1);

            HistoryPage historyPage = new HistoryPage(webDriver);
            historyPage.GoToPage();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var list = historyPage.GetLastTransaction();

            Assert.IsTrue(list[0] == "vlad12345@gmail.com");
            Assert.IsTrue(list[1] == "1");
            Assert.IsTrue(list[2] == "RON");

        }

        [TestMethod]
        public void VerifyLogoutAction()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("vlad123@gmail.com", "vlad123!A");
            IAlert alertf = webDriver.SwitchTo().Alert();
            alertf.Accept();

            homePage.GoToPage();
            homePage.LogOut();
            Assert.AreEqual(webDriver.Title, "Log in - MyBank");

        }

        [TestMethod]
        public void VerifyChangePhoneNumber()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("vlad123@gmail.com", "vlad123!A");
            IAlert alertf = webDriver.SwitchTo().Alert();
            alertf.Accept();

            ManagePage managePage = new ManagePage(webDriver);
            managePage.GoToPage();
            managePage.SavePhoneNumber("0745101010");
            managePage.GoToPage();
            Assert.AreEqual("0745101010", managePage.GetPhoneNumber());
        }


        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            webDriver.Close();
        }
    }
}
