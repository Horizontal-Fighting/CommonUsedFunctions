using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Account : BaseEntity
    {
        public AccountType AccountType { get; set; }

        public virtual CashAccount CashAccount { get; set; }
    }
}
