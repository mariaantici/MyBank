using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBank.Controllers;
using MyBank.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace MyBank.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        [Fact]
        public void PostTransfer_RedirectToFailurePage_GivenIncorrectUsername()
        {
            //Arrange
            var transactionServiceMock = new Mock<ITransactionService>();

            transactionServiceMock
                .Setup(r => r.validUsername(It.IsAny<string>()))
                .Returns(false);

            var transactionsController = new TransactionsController(transactionServiceMock.Object, null, null);


            //Act
            var response = (RedirectToActionResult)transactionsController.PostTransfer(null, 0, null);

            //Assert

            Assert.IsAssignableFrom<RedirectToActionResult>(response);
            Assert.Equal("Failure", response.ActionName);
        }   
        
        [Fact]
        public void PostTransfer_RedirectToFailurePage_GivenIncorrectAmount()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Radu"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            var transactionServiceMock = new Mock<ITransactionService>();
            var accountServiceMock = new Mock<IAccountsService>();

                //Mock Transaction
            transactionServiceMock
                .Setup(r => r.validUsername(It.IsAny<string>()))
                .Returns(true);

            transactionServiceMock
                .Setup(r => r.validBalanceAmount(It.IsAny<int>(), It.IsAny<float>(), It.IsAny<string>()))
                .Returns(false);


                //Mock Account
            accountServiceMock
                .Setup(r => r.GetAccountId(It.IsAny<string>()))
                .Returns(1);

            var transactionsController = new TransactionsController(transactionServiceMock.Object, accountServiceMock.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            //Act
            var response = (RedirectToActionResult)transactionsController.PostTransfer("user@gmail.com", 0, null);

            //Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(response);
            Assert.Equal("Failure", response.ActionName);
        }        
        
        [Fact]
        public void PostTransfer_RedirectToSuccessPage_GivenValidParameters()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Radu"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            var transactionServiceMock = new Mock<ITransactionService>();
            var accountServiceMock = new Mock<IAccountsService>();
            var historyServiceMock = new Mock<IHistoryService>();

                //Mock Transaction

            transactionServiceMock
                .Setup(r => r.validUsername(It.IsAny<string>()))
                .Returns(true);

            transactionServiceMock
                .Setup(r => r.validBalanceAmount(It.IsAny<int>(), It.IsAny<float>(), It.IsAny<string>()))
                .Returns(true);

                //Mock Account

            accountServiceMock
            .Setup(r => r.GetAccountId(It.IsAny<string>()))
            .Returns(1);

            accountServiceMock
                .Setup(r => r.GetUserIDforUsername(It.IsAny<string>()))
                .Returns("2");


            var transactionsController = new TransactionsController(transactionServiceMock.Object, accountServiceMock.Object, historyServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            //Act
            var response = (RedirectToActionResult)transactionsController.PostTransfer("user@gmail.com", 0, null);

            //Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(response);
            Assert.Equal("Success", response.ActionName);
        }
    }
}