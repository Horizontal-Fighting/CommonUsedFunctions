using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class MarginAccount:BaseEntity
    {
        public MarginAccount()
        {
            currencyType = CurrencyType.USD;
            CashAccount = new CashAccount();
        }
        private CurrencyType currencyType;

        public MainAccount MainAccount { get; set; }

        /// <summary>
        /// 现金、股票、与期权价值之和
        /// </summary>
        public decimal TotalAssets { get; set; }

        /// <summary>
        /// 现金额;已结算的现金、融券（做空）所得资金和卖出期权权利金之和。
        /// </summary>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// 证券总价值; 做多持仓与做空持仓的绝对价值总和;
        /// </summary>
        public decimal PositionValue { get; set; }

        /// <summary>
        /// 持仓盈亏，持仓中每股、美式期权、港股、A股的盈亏总计;
        /// </summary>
        public decimal UnrealizedPnL { get; set; }


        /// <summary>
        /// 现金分账户相关信息，包含多种账户，如：人民币、美元、港币
        /// </summary>
        public CashAccount CashAccount { get; set; }

        public CurrencyType CurrencyType
        {
            get
            {
                return currencyType;
            }
        }


    }
}
