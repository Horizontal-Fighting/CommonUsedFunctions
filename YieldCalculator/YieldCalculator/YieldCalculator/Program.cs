using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            ////入金为负值，出金为正值
            //double[] payments = { -6800, 1000, 2000, 4000 }; // payments
            //double[] days = { 01, 08, 16, 25 }; // days of payment (as day of year)
            //double xirr = XirrCalculator.Newtons_method(0.1,
            //                             XirrCalculator.total_f_xirr(payments, days),
            //                             XirrCalculator.total_df_xirr(payments, days));

            List<TransactionModel> transactionModelList = new List<TransactionModel>() {
                new TransactionModel() {
                    TransactionAmountUSD=6800,
                    TransactionType=TransactionType.Deposit,
                    TransactionDateUtc=new DateTime(2017,1,1)
                },
                new TransactionModel() {
                    TransactionAmountUSD = 1000,
                    TransactionType=TransactionType.WithDraw,
                    TransactionDateUtc=new DateTime(2017,2,1)
                },
                new TransactionModel() {
                    TransactionAmountUSD = 2000,
                    TransactionType=TransactionType.WithDraw,
                    TransactionDateUtc=new DateTime(2017,3,1)
                },
                new TransactionModel() {
                    TransactionAmountUSD = 4000,
                    TransactionType=TransactionType.WithDraw,
                    TransactionDateUtc=new DateTime(2017,4,1)
                }
            };

            double xirr = XirrCalculator.Xirr(transactionModelList);


            Console.WriteLine("XIRR value is {0}", xirr);
            Console.ReadKey();

        }
    }
}
