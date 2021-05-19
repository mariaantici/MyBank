using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyBank.Models;
using MyBank.Services;
using Xunit;
using Moq;
using MyBank.Data;

namespace TestsMaria
{
    public class UnitTest1
    {
        //Test 1

        [Fact]
        public void Verify_GetTransactions()
        {
            var dataAccount = new List<Account>
            {
                new Account("Maria"),
            }.AsQueryable();

            dataAccount.FirstOrDefault(x => x.UserID.Equals("Maria")).AccountID = 100;
            var mockSet1 = new Mock<DbSet<Account>>();
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(dataAccount.Provider);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(dataAccount.Expression);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(dataAccount.ElementType);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(dataAccount.GetEnumerator());

            var dataTransactions = new List<Transaction>
            {
                new Transaction(100, 52, DateTime.Now, "EUR", 10),
                new Transaction(100, 52, DateTime.Now, "EUR", 10),
                new Transaction(100, 52, DateTime.Now, "EUR", 10),
                new Transaction(100, 52, DateTime.Now, "EUR", 10),
                new Transaction(100, 52, DateTime.Now, "EUR", 10),
                new Transaction(100, 52, DateTime.Now, "EUR", 10),
            }.AsQueryable();

            var mockSet2 = new Mock<DbSet<Transaction>>();
            mockSet2.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(dataTransactions.Provider);
            mockSet2.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(dataTransactions.Expression);
            mockSet2.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(dataTransactions.ElementType);
            mockSet2.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(dataTransactions.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Accounts).Returns(mockSet1.Object);
            mockContext.Setup(m => m.Transactions).Returns(mockSet2.Object);

            var service = new HistoryService(mockContext.Object);

            Assert.Equal(6, service.GetTransactions("Maria").Count);

        }


        //Test 2

        [Fact]
        public void Verify_GetBalancesForAccountId()
        {
            var dataAccount = new List<Account>
            {
                new Account("Maria"),
            }.AsQueryable();

            dataAccount.FirstOrDefault(x => x.UserID.Equals("Maria")).AccountID = 100;
            var mockSet1 = new Mock<DbSet<Account>>();
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(dataAccount.Provider);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(dataAccount.Expression);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(dataAccount.ElementType);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(dataAccount.GetEnumerator());

            var dataBalances = new List<Balance>
            {
                new Balance(),
                new Balance(),
            }.AsQueryable();

            dataBalances.ToList().ForEach(c => c.AccountID = 100);

            var mockSet2 = new Mock<DbSet<Balance>>();
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.Provider).Returns(dataBalances.Provider);
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.Expression).Returns(dataBalances.Expression);
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.ElementType).Returns(dataBalances.ElementType);
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.GetEnumerator()).Returns(dataBalances.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Accounts).Returns(mockSet1.Object);
            mockContext.Setup(m => m.Balances).Returns(mockSet2.Object);

            var service = new AccountsService(mockContext.Object);

            Assert.Equal(2, service.GetBalancesForAccountId(100).Count);

        }


        //Test 3

        [Fact]
        public void Verify_GetBalanceForAccountIdAndCurrency()
        {
            var dataAccount = new List<Account>
            {
                new Account("Maria"),
            }.AsQueryable();

            dataAccount.FirstOrDefault(x => x.UserID.Equals("Maria")).AccountID = 100;
            var mockSet1 = new Mock<DbSet<Account>>();
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(dataAccount.Provider);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(dataAccount.Expression);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(dataAccount.ElementType);
            mockSet1.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(dataAccount.GetEnumerator());

            var dataBalances = new List<Balance>
            {
                new Balance(),
            }.AsQueryable();

            dataBalances.FirstOrDefault().AccountID = 100;
            dataBalances.FirstOrDefault().Currency = "TestCurrency";

            var mockSet2 = new Mock<DbSet<Balance>>();
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.Provider).Returns(dataBalances.Provider);
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.Expression).Returns(dataBalances.Expression);
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.ElementType).Returns(dataBalances.ElementType);
            mockSet2.As<IQueryable<Balance>>().Setup(m => m.GetEnumerator()).Returns(dataBalances.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Accounts).Returns(mockSet1.Object);
            mockContext.Setup(m => m.Balances).Returns(mockSet2.Object);

            var service = new AccountsService(mockContext.Object);

            Assert.Equal("TestCurrency", service.GetBalanceForAccountIdAndCurrency(100, "TestCurrency").Currency);

        }
    }
}
