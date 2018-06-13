using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public abstract class  Account : BaseEntity
    {
        public AccountType AccountType { get; set; }

        public virtual CashAccount CashAccount { get; set; }

        public virtual Commisions HistoricalCommisions { get; set; }
    }

    public class MainAccount : Account
    {
        public MainAccount()
        {
            base.AccountType = AccountType.Main;
        }

        public MarginAccount MarginAccount { get; set; }

        

        /// <summary>
        /// 从券商获取账户信息
        /// </summary>
        /// <returns></returns>
        public MarginAccount GetMarginAccountFromBroker()
        {
            //todo
            return null;
        }
    }

    public class PersonalAccount : Account
    {
        public PersonalAccount()
        {
            base.AccountType = AccountType.Personal;
        }

        public String Note { get; set; }
    }
}
