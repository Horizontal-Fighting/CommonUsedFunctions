using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldCalculator
{
    public class TransactionModel
    {
        public DateTime TransactionDateUtc { get; set; }

        public decimal TransactionAmountUSD { get; set; }
        
        public TransactionType TransactionType { get; set; }
    }




    public enum TransactionType
    {
        WithDraw,
        Deposit
    }





}
