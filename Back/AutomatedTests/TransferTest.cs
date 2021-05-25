
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using SeleniumExtras.PageObjects;
using AutomatedTests.PageObjects;
using System.Reflection;
using System.IO;
using OpenQA.Selenium.Chrome;

namespace AutomatedTests
{
    [TestClass]
    public class TransferTest
    {

        private IWebDriver webDriver;

        [TestInitialize]
        public void InitTests()
        {
            webDriver = new ChromeDriver(Environment.CurrentDirectory);
        }

        [TestMethod]
        public void VerifyLoginPage()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.GoToPage();
            LoginPage loginPage = homePage.GoToLoginPage();
            loginPage.Login("vlad123@gmail.com", "vlad123!A");

            Assert.IsTrue(true);


        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
