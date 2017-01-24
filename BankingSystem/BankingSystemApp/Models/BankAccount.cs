using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystemApp.Models
{
    public class BankAccount
    {

        public BankAccount()
        {
            this.balance = 0m;
        }
        public BankAccount(string ssn,string accountNumber,decimal balance)
        {
            this.SSN = ssn;
            this.AccountNumber = accountNumber;

            if (balance < 100m)
                throw new ArgumentOutOfRangeException("balance should be more than $100.");

            this.balance = balance;
        }

        public string SSN { get; set; }


        public string AccountNumber { get; set; }

        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        private decimal balance;


        private object thisLock = new object();

        public decimal Deposit(decimal money)
        {
            lock(thisLock)
            {
                if (money <= 0m)
                    throw new ArgumentOutOfRangeException("Deposit should more than $0.");
                this.balance = this.Balance + money;
                return this.Balance;
            }
           
        }

        public decimal WithDraw(decimal money)
        {
            lock (thisLock)
            {
                if (money <= 0m)
                    throw new ArgumentOutOfRangeException("Withdraw should more than $0.");

                if (money > this.Balance * 0.9m)
                    throw new ArgumentOutOfRangeException("Withdraw more than 90% money at one time is not allowed.");

                if (money > 1000m)
                    throw new ArgumentOutOfRangeException("Withdraw more than $1000 at one time is not allowed.");


                decimal currentBalance = Balance - money;
                if (currentBalance < 100m)
                    throw new ArgumentOutOfRangeException("Balance must more than $100.");

                this.balance = currentBalance;
                return Balance;
            }
        }

        public override int GetHashCode()
        {
            return 17 * AccountNumber.GetHashCode() + 19 * AccountNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BankAccount))
                return false;
            else
            {
                BankAccount o = new BankAccount();
                o = (BankAccount)obj;

                if (o.SSN == this.SSN && o.Balance == this.Balance && o.AccountNumber == this.AccountNumber)
                    return true;
                else
                    return false;
            }
        }
    }
}