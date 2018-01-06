using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldCalculator
{
    public class XirrCalculatorUtility
    {
        public const double tol = 0.001;
        public delegate double fx(double x);

        public static fx composeFunctions(fx f1, fx f2)
        {
            return (double x) => f1(x) + f2(x);
        }

        public static fx f_xirr(double p, double dt, double dt0)
        {
            return (double x) => p * Math.Pow((1.0 + x), ((dt0 - dt) / 365.0));
        }

        public static fx df_xirr(double p, double dt, double dt0)
        {
            return (double x) => (1.0 / 365.0) * (dt0 - dt) * p * Math.Pow((x + 1.0), (((dt0 - dt) / 365.0) - 1.0));
        }

        public static fx total_f_xirr(double[] payments, double[] days)
        {
            fx resf = (double x) => 0.0;

            for (int i = 0; i < payments.Length; i++)
            {
                resf = composeFunctions(resf, f_xirr(payments[i], days[i], days[0]));
            }

            return resf;
        }

        public static fx total_df_xirr(double[] payments, double[] days)
        {
            fx resf = (double x) => 0.0;

            for (int i = 0; i < payments.Length; i++)
            {
                resf = composeFunctions(resf, df_xirr(payments[i], days[i], days[0]));
            }

            return resf;
        }

        public static double Xirr(double guess, fx f, fx df)
        {
            double x0 = guess;
            double x1 = 0.0;
            double err = 1e+100;

            while (err > tol)
            {
                x1 = x0 - f(x0) / df(x0);
                err = Math.Abs(x1 - x0);
                x0 = x1;
            }

            return x0;
        }


        public static double Xirr(IEnumerable<TransactionModel> transactionModels)
        {
            List<double> paymentList = new List<double>();
            List<double> dayList = new List<double>() ;

            TransactionModel firstTransaction = transactionModels.ToList().FirstOrDefault();

            foreach (var tmpTransaction in transactionModels)
            {
                if (tmpTransaction.TransactionType == TransactionType.Deposit)
                    paymentList.Add((double)-tmpTransaction.TransactionAmountUSD);
                else
                    paymentList.Add((double)tmpTransaction.TransactionAmountUSD);

                //Days是int; TotalDays是double
                dayList.Add((tmpTransaction.TransactionDateUtc-firstTransaction.TransactionDateUtc).Days);
            }

            return Xirr(0.1,
                                XirrCalculatorUtility.total_f_xirr(paymentList.ToArray(), dayList.ToArray()),
                                XirrCalculatorUtility.total_df_xirr(paymentList.ToArray(), dayList.ToArray()));

        }

    }
}
