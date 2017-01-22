using BankingSystemApp.Controllers;
using BankingSystemApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankingSystemApp.Tests
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void GetAllAccounts_ShouldReturnAllAccounts()
        {
            var accounts = GetAllTestAccounts();
            var controller = new BankAccountController(accounts);

            var result = controller.GetAllAccounts();
            int countRec = 0;
            foreach (var tmp in result.Result)
            {
                countRec ++;
            }
            Assert.AreEqual(accounts.Count, countRec);
        }



        [TestMethod]
        public void CreateAccount_ShouldReturnCreatedAccount()
        {
            var accounts = GetAllTestAccounts();
            var controller = new BankAccountController(accounts);

            BankAccount newAccount = new BankAccount("123456789", "890123", 999m);

            var result = controller.CreateAccount("123456789", "890123", 999m);
            Type ts = result.Result.GetType();
            var resultObj = ts.GetProperty("Content").GetValue(result.Result);
            Assert.AreEqual(newAccount, resultObj);
        }

        [TestMethod]
        public void DeleteAccount_ShouldReturnCreatedAccount()
        {
            var accounts = GetAllTestAccounts();
            var controller = new BankAccountController(accounts);

            var result = controller.DeleteAccount("321467");
            Type ts = result.Result.GetType();
            var resultObj = ts.GetProperty("Content").GetValue(result.Result);
            Assert.AreEqual(1, resultObj);
        }

        [TestMethod]
        public void WithDraw_ShouldReturnUpdatedAccount()
        {
            List<BankAccount> accounts = GetAllTestAccounts();
            var controller = new BankAccountController(accounts);

            var targetAccount = accounts.Find(r => r.AccountNumber == "123456");
            targetAccount.WithDraw(50m); // = targetAccount.Balance - 50m;

            var result = controller.WithDraw("123456",50m);
            Type ts = result.Result.GetType();
            var resultObj = ts.GetProperty("Content").GetValue(result.Result);
            Assert.AreEqual(targetAccount, resultObj);
        }

        [TestMethod]
        public void Deposit_ShouldReturnUpdatedAccount()
        {
            List<BankAccount> accounts = GetAllTestAccounts();
            var controller = new BankAccountController(accounts);

            var targetAccount = accounts.Find(r => r.AccountNumber == "123456");
            targetAccount.Deposit(50m); // = targetAccount.Balance + 50m;

            var result = controller.WithDraw("123456", 50m);
            Type ts = result.Result.GetType();
            var resultObj = ts.GetProperty("Content").GetValue(result.Result);
            Assert.AreEqual(targetAccount, resultObj);
        }


        private List<BankAccount> GetAllTestAccounts()
        {
            var bankAccounts = new List<BankAccount>();
            bankAccounts.Add(new BankAccount("123456789", "123456",200m));
            bankAccounts.Add(new BankAccount("123456789", "123452", 500m));
            bankAccounts.Add(new BankAccount("123456789", "321467", 700m));
            bankAccounts.Add(new BankAccount("123456789", "753246", 2000m));
            return bankAccounts;
        }


    }
}
