using BankingSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BankingSystemApp.Controllers
{
    public class BankAccountController : ApiController
    {
        List<BankAccount> accounts = new List<BankAccount>();

        public BankAccountController() { }

        public BankAccountController(List<BankAccount> accounts)
        {
            this.accounts = accounts;
        }

        public async Task<IEnumerable<BankAccount>> GetAllAccounts()
        {
            return await Task.FromResult(accounts);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateAccount(string ssn, string accountNumber, decimal balance)
        {
            try
            {
                BankAccount result = new BankAccount(ssn,accountNumber,balance);
                accounts.Add(result);
                return await Task.FromResult(Ok(result));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(InternalServerError(ex));
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAccount(string accountNumber)
        {
            try
            {
                var result = accounts.RemoveAll(r => r.AccountNumber == accountNumber);
                return await Task.FromResult(Ok(result));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(InternalServerError(ex));
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Deposit(string accountNumber, decimal money)
        {
            try
            {
                var targetAccount = accounts
                    .Where(r => r.AccountNumber == accountNumber)
                    .Select(r=>r)
                    .FirstOrDefault();

                if (targetAccount == null)
                    return await Task.FromResult(InternalServerError(new Exception("can not find account:"+ accountNumber)));
                
                if(money<0)
                    return await Task.FromResult(InternalServerError(new Exception("deposit should be more than 0.")));

                targetAccount.Deposit(money);

                return await Task.FromResult(Ok(targetAccount));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(InternalServerError(ex));
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> WithDraw(string accountNumber, decimal money)
        {
            try
            {
                var targetAccount = accounts
                    .Where(r => r.AccountNumber == accountNumber)
                    .Select(r => r)
                    .FirstOrDefault();

                if (targetAccount == null)
                    return await Task.FromResult(InternalServerError(new Exception("can not find account:" + accountNumber)));

                if (money < 0)
                    return await Task.FromResult(InternalServerError(new Exception("WithDraw should be more than 0.")));

                targetAccount.WithDraw(money);// -= money;

                return await Task.FromResult(Ok(targetAccount));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(InternalServerError(ex));
            }
        }

    }
}
