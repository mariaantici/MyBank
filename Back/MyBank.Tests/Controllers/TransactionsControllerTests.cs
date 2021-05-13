using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBank.Controllers;
using MyBank.Services;
using System;
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
    }
}