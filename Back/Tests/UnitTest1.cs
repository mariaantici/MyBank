using Microsoft.EntityFrameworkCore;
using Moq;
using MyBank.Data;
using MyBank.Models;
using MyBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Verify_AddHistoryEntry()
        {
            var mockSet = new Mock<DbSet<Transaction>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Transactions).Returns(mockSet.Object);

            var service = new HistoryService(mockContext.Object);
            service.AddHistoryEntry(It.IsAny<int>(), It.IsAny<int>(), DateTime.Now, It.IsAny<string>(), It.IsAny<float>());

            mockSet.Verify(m => m.Add(It.IsAny<Transaction>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Verify_GetAccountId()
        {
            var data = new List<Account>
            {
                new Account("Vlad"),
            }.AsQueryable();

            data.FirstOrDefault(x => x.UserID.Equals("Vlad")).AccountID = 100;
            var mockSet1 = new Mock<DbSet<Account>>();
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Accounts).Returns(mockSet1.Object);
       
            var service = new AccountsService(mockContext.Object);

            Assert.Equal(100, service.GetAccountId("Vlad"));
           
        }

        [Fact]
        public void Verify_GetUserNameForAccountId()
        {
            var dataAccount = new List<Account>
            {
                new Account("Vlad"),
            }.AsQueryable();
            dataAccount.FirstOrDefault(x => x.UserID.Equals("Vlad")).AccountID = 100;

            var dataUser = new List<ApplicationUser>
            {
                new ApplicationUser(),
            }.AsQueryable();
            dataUser.FirstOrDefault().UserName = "Vlad";
            dataUser.FirstOrDefault().Id = "Vlad";

            var mockSetAccount = new Mock<DbSet<Account>>();
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(dataAccount.Provider);
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(dataAccount.Expression);
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(dataAccount.ElementType);
            mockSetAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(dataAccount.GetEnumerator());
            var mockSetUser = new Mock<DbSet<ApplicationUser>>();
            mockSetUser.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(dataUser.Provider);
            mockSetUser.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(dataUser.Expression);
            mockSetUser.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(dataUser.ElementType);
            mockSetUser.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(dataUser.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Accounts).Returns(mockSetAccount.Object);
            mockContext.Setup(m => m.Users).Returns(mockSetUser.Object);

            var service = new AccountsService(mockContext.Object);

            Assert.Equal("Vlad", service.GetUserNameForAccountId(100));

        }
    }
}
