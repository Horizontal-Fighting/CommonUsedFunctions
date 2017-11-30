using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    /// <summary>
    /// 单一币种的现金账户
    /// </summary>
    public class SingleCurrencyCashAccount : BaseEntity
    {
        public virtual CashAccount CashAccount { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public decimal Balance { get; set; }
    }
}
