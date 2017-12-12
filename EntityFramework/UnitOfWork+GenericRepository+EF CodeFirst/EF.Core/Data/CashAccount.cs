using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class CashAccount : BaseEntity
    {
        public CashAccount()
        {
            SingleCurrencyCashAccounts = new List<SingleCurrencyCashAccount>();
        }

        public virtual Account Account { get; set; }


        public virtual ICollection<SingleCurrencyCashAccount> SingleCurrencyCashAccounts { get; set; }


        /// <summary>
        /// 获取美元计价的总账户额度
        /// </summary>
        /// <returns></returns>
        public SingleCurrencyCashAccount GetTotalCashUSD()
        {
            //TODO
            //CurrencyExchangeRateConverter.RateConvert();
            return null;
        }
    }
}
