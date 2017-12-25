using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public enum CurrencyType
    {
        USD = 1,
        HKD = 2,
        CNY = 3
    }

    public enum AccountType
    {
        Main = 1,
        Personal = 2,
        Organization = 3
    }

    public enum RoleType
    {
        Admin = 1,
        GeneralPartner = 2,
        LimitedPartner = 3,
        Public = 4
    }


}
